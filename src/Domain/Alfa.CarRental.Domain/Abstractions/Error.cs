namespace Alfa.CarRental.Domain.Abstractions;

public record Error(string ErrorCode, string ErrorDescription)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static readonly Error NullValue = new("Error.NullValue", "A null value was entered");
}
