using Microsoft.EntityFrameworkCore;

using Alfa.CarRental.Domain.Rentals;
using Alfa.CarRental.Domain.Vehicles;

namespace Alfa.CarRental.Infrastructure.Repositories;

internal sealed class RentalRepository : Repository<Rental>, IRentalRepository
{
    private static readonly RentalStatus[] ActiveRentalStatus = { 
        RentalStatus.Reserved,
        RentalStatus.Confirmed,
        RentalStatus.Completed
    };

    public RentalRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsOverlappingAsync(Vehicle vehicle, DateRange dateRange, CancellationToken cancellationToken = default)
    {
        return await DbContext.Set<Rental>()
            .AnyAsync(
                rental => rental.VehicleId == vehicle.Id
                    && rental.DateRange.StartDate == dateRange.EndDate
                    && rental.DateRange.EndDate == dateRange.StartDate
                    && ActiveRentalStatus.Contains(rental.Status),
                cancellationToken
            );
    }
}
