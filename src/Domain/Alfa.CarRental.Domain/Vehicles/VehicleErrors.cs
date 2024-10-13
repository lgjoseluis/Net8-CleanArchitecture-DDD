using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Domain.Vehicles;

public static class VehicleErrors
{
    public static readonly Error NotFound = new("Vehicle.NotFound", "Vehicle not found");
}
