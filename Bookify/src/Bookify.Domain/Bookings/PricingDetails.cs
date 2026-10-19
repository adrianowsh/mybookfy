using Bookify.Domain.Shareds;

namespace Bookify.Domain.Bookings;

public sealed record class PricingDetails(
	Money PriceForPeriod,
	Money CleaningFee,
	Money AmenityUpCharge,
	Money TotalPrice
);