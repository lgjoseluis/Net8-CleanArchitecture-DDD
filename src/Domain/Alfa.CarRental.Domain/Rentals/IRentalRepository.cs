using Alfa.CarRental.Domain.Vehicles;

namespace Alfa.CarRental.Domain.Rentals;

public interface IRentalRepository
{
    Task<Rental> GetByIdAsync( Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(Vehicle vehicle, DateRange range, CancellationToken cancellationToken = default);

    void Add(Rental rental);
}
