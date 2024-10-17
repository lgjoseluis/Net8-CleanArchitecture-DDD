using Alfa.CarRental.Domain.Vehicles;

namespace Alfa.CarRental.Infrastructure.Repositories;

internal sealed class VehicleRepository : Repository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
