namespace Bookify.Domain.Shareds;

public sealed record class Currency
{
	internal static readonly Currency None = new("");
	public static readonly Currency USD = new("USD");
	public static readonly Currency BRL = new("BRL");
	private Currency(string code)
	{
		Code = code;
	}

	public string Code { get; init; }

	public static readonly IReadOnlyCollection<Currency> All =
	[
		USD,
		BRL
	];

	public static Currency FromCode(string code) =>
		All.FirstOrDefault(c => c.Code == code) ??
		throw new ArgumentException($"Invalid currency code: {code}");
}
