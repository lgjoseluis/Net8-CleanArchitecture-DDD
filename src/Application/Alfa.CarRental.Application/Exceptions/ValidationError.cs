namespace Alfa.CarRental.Application.Exceptions;

public sealed record ValidationError(
    string PropertyName,
    string ErrorMessage
    );
