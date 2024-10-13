namespace Alfa.CarRental.Domain.Abstractions;

public record Error(string ErrorCode, string ErrorDescription)
{
    public static Error None = new(string.Empty, string.Empty);

    public static Error NullValue = new("Error.NullValue", "A null value was entered");
}
