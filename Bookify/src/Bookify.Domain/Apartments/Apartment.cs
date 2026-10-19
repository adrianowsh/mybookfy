using Bookify.Domain.Abstractions;
using Bookify.Domain.Shareds;

namespace Bookify.Domain.Apartments;

public sealed class Apartment : Entity<string>
{
    private Apartment(
        Name name,
        Description description,
        Address address,
        Money price,
        Money cleaningFee,
        Amenity[] amenities)
        : base(SetNewId())
    {
        Name = name;
        Description = description;
        Address = address;
        Price = price;
        CleaningFee = cleaningFee;
        Amenities = amenities;
    }
    public Name Name { get; private set; }
    public Description Description { get; private set; }
    public Address Address { get; private set; }
    public Money Price { get; private set; }
    public Money CleaningFee { get; private set; }
    public DateTime? LastBookedOnUtc { get; internal set; }
    public Amenity[] Amenities { get; private set; }
    public static string SetNewId()
    {
        return Guid.CreateVersion7().ToString();
    }
    public static Apartment Create(
        Name name,
        Description description,
        Address address,
        Money price,
        Money cleaningFee,
        Amenity[] amenities)
    {
        return new Apartment(
            name,
            description,
            address,
            price,
            cleaningFee,
            amenities);
    }
}
