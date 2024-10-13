namespace Alfa.CarRental.Domain.Vehicles;

public interface IVehicleRepository
{
    Task<Vehicle?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
