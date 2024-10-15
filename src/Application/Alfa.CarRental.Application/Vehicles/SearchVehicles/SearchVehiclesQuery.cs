using Alfa.CarRental.Application.Abstractions.Messaging;

namespace Alfa.CarRental.Application.Vehicles.SearchVehicles;

public sealed record SearchVehiclesQuery(DateOnly StartDate, DateOnly EndDate):IQuery<IReadOnlyList<VehicleResponse>>;
