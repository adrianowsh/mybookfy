namespace Bookify.Domain.Apartments;

public sealed record Address(
    string Street,
    string State,
    string City,
    string ZipCode,
    string Country);