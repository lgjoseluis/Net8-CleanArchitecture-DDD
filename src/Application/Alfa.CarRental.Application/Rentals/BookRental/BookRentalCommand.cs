using Alfa.CarRental.Application.Abstractions.Messaging;

namespace Alfa.CarRental.Application.Rentals.BookRental;

public record BookRentalCommand(
    Guid VehicleId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
    ) : ICommand<Guid>;    
