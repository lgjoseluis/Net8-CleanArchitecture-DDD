using Alfa.CarRental.Application.Abstractions.Clock;

namespace Alfa.CarRental.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime CurrentTime => DateTime.UtcNow;
}
