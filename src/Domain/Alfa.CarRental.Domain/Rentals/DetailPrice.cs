using Alfa.CarRental.Domain.Vehicles;

namespace Alfa.CarRental.Domain.Rentals;

public record DetailPrice
(
    Currency Price,
    Currency MaintenanceCost,
    Currency AccessoriesCost,
    Currency TotalPrice
);
