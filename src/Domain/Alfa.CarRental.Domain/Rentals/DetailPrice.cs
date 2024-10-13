using Alfa.CarRental.Domain.Shared;

namespace Alfa.CarRental.Domain.Rentals;

public record DetailPrice
(
    Currency Price,
    Currency MaintenanceCost,
    Currency AccessoriesCost,
    Currency TotalPrice
);
