using Alfa.CarRental.Domain.Vehicles;

namespace Alfa.CarRental.Domain.Rentals;

public interface IRentalRepository
{
    Task<Rental?> FindByIdAsync( Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsOverlappingAsync(Vehicle vehicle, DateRange dateRange, CancellationToken cancellationToken = default);

    Task AddAsync(Rental rental);
}
