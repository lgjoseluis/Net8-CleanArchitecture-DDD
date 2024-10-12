namespace Alfa.CarRental.Domain.Vehicles;

public record CurrencyType
{
    public static readonly CurrencyType NONE = new CurrencyType("");
    public static readonly CurrencyType USD = new CurrencyType("USD");
    public static readonly CurrencyType EUR = new CurrencyType("EUR");

    public string? Code {  get; init; }

    public CurrencyType(string code)
    {
        Code = code;
    }

    public static readonly IReadOnlyCollection<CurrencyType> All = new[]
    {
        USD,
        EUR
    };

    public static CurrencyType FromCode(string code)
    { 
        return All.FirstOrDefault(x => x.Code == code) ?? throw new ApplicationException("The currency type is incorrect");
    }
}
