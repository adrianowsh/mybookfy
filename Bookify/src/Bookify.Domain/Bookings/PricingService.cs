using System;
using Bookify.Domain.Apartments;
using Bookify.Domain.Shareds;

namespace Bookify.Domain.Bookings;

public sealed class PricingService
{
	public PricingDetails CalculatePrice(Apartment apartment, DateRange period)
	{
		Currency currency = apartment.Price.Currency;
		var priceForPeriod = new Money(
			apartment.Price.Amount * period.LengthInDays,
			currency);

		decimal percentageUpCharge = 0;
		foreach (Amenity amenity in apartment.Amenities)
		{
			percentageUpCharge += amenity switch
			{
				Amenity.MountainView => 10,
				Amenity.Gym => 5,
				Amenity.Spa => 15,
				_ => 0
			};
		}

		var amenityUpCharge = Money.Zero();
		if (percentageUpCharge > 0)
		{
			amenityUpCharge = new Money(
				priceForPeriod.Amount * percentageUpCharge,
				currency);
		}

		var totalPrice = Money.Zero();
		totalPrice += priceForPeriod;

		if (!apartment.CleaningFee.IsZero())
		{
			totalPrice += apartment.CleaningFee;
		}

		totalPrice += amenityUpCharge;
		return new PricingDetails(priceForPeriod, apartment.CleaningFee, amenityUpCharge, totalPrice);
	}
}
