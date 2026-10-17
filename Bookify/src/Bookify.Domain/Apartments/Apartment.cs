using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Apartments;

public sealed class Apartment : Entity<string>
{
    private Apartment(
        string name,
        string description,
        Address address,
        decimal priceAmount,
        string priceCurrency,
        decimal cleaningFeeAmount,
        string cleaningFeeCurrency) : base(SetNewId())
    {
        Name = name;
        Description = description;
        Address = address;
        PriceAmount = priceAmount;
        CleaningFeeAmount = cleaningFeeAmount;
        CleaningFeeCurrency = cleaningFeeCurrency;
        PriceCurrency = priceCurrency;
    }

    public string Name { get; init; }
    public string Description { get; init; }
    public Address Address { get; init; }
    public decimal PriceAmount { get; init; }
    public string PriceCurrency { get; init; }
    public decimal CleaningFeeAmount { get; init; }
    public string CleaningFeeCurrency { get; init; }
    public DateTime? LastBookedOnUtc { get; set; }
    public Amenity[] Amenities { get; set; } = [];

    public static string SetNewId()
    {
        return Guid.CreateVersion7().ToString();
    }

    public static Apartment NewApartment(
        string name,
        string description,
        Address address,
        decimal priceAmount,
        string priceCurrency,
        decimal cleaningFeeAmount,
        string cleaningFeeCurrency)
    {
        return new Apartment(
            name,
            description,
            address,
            priceAmount,
            priceCurrency,
            cleaningFeeAmount,
            cleaningFeeCurrency);
    }
}
