using System;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Shareds;

namespace Bookify.Domain.Bookings;

public sealed class Booking : Entity<string>
{
	private Booking(
		string id,
		string apartmentId,
		string userId,
		 DateRange duration,
		 Money pricePerPeriod,
		 Money cleaningFee,
		 Money amenitiesUpCharge,
		 Money totalPrice,
		 BookingStatus status,
		 DateTime createdOnUtc)
		: base(id)
	{
		ApartmentId = apartmentId;
		UserId = userId;
		Duration = duration;
		PricePerPeriod = pricePerPeriod;
		CleaningFee = cleaningFee;
		AmenitiesUpCharge = amenitiesUpCharge;
		TotalPrice = totalPrice;
		Status = status;
		CreatedOnUtc = createdOnUtc;
	}
	public string ApartmentId { get; private set; }
	public string UserId { get; private set; }
	public DateRange Duration { get; private set; }
	public Money PricePerPeriod { get; private set; }
	public Money CleaningFee { get; private set; }
	public Money AmenitiesUpCharge { get; private set; }
	public Money TotalPrice { get; private set; }
	public BookingStatus Status { get; private set; }
	public DateTime CreatedOnUtc { get; private set; }
	public DateTime? ConfirmedOnUtc { get; private set; }
	public DateTime? CancelledOnUtc { get; private set; }
	public DateTime? RejectedOnUtc { get; private set; }
	public DateTime? CompletedOnUtc { get; private set; }
	public static Booking Create(
		string id,
		string apartmentId,
		string userId,
		DateRange duration,
		Money pricePerPeriod,
		Money cleaningFee,
		Money amenitiesUpCharge,
		Money totalPrice,
		BookingStatus status,
		DateTime createdOnUtc)
	{
		Booking booking = new(
			id,
			apartmentId,
			userId,
			duration,
			pricePerPeriod,
			cleaningFee,
			amenitiesUpCharge,
			totalPrice,
			status,
			createdOnUtc);
		return booking;
	}

	public static Booking Reserve(
		Apartment apartment,
		string userId,
		DateRange duration,
		DateTime utcNow,
		PricingService pricingService)
	{
		PricingDetails pricingDetails = pricingService.CalculatePrice(apartment, duration);

		Booking booking = Create(
			$"bok_{Guid.CreateVersion7()}",
			apartment.Id,
			userId,
			duration,
			pricingDetails.PriceForPeriod,
			pricingDetails.CleaningFee,
			pricingDetails.AmenityUpCharge,
			pricingDetails.TotalPrice,
			BookingStatus.Reserved,
			utcNow);

		booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));

		apartment.LastBookedOnUtc = utcNow;

		return booking;
	}
	public void Cancel()
	{
		if (Status != BookingStatus.Reserved && Status != BookingStatus.Confirmed)
		{
			throw new InvalidOperationException("Only reserved or confirmed bookings can be cancelled.");
		}
		Status = BookingStatus.Cancelled;
		CancelledOnUtc = DateTime.UtcNow;
	}

	public void Reject()
	{
		if (Status != BookingStatus.Reserved)
		{
			throw new InvalidOperationException("Only reserved bookings can be rejected.");
		}
		Status = BookingStatus.Rejected;
		RejectedOnUtc = DateTime.UtcNow;
	}

	public void Complete()
	{
		if (Status != BookingStatus.Confirmed)
		{
			throw new InvalidOperationException("Only confirmed bookings can be completed.");
		}
		Status = BookingStatus.Completed;
		CompletedOnUtc = DateTime.UtcNow;
	}

}
