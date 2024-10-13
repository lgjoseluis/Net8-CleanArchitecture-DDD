namespace Alfa.CarRental.Application.Abstractions.Clock;

public  interface IDateTimeProvider
{
    DateTime CurrentTime { get; }
}
