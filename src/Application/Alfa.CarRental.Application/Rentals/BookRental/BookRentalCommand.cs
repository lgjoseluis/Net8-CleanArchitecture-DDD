using Alfa.CarRental.Application.Abstractions.Messaging;

namespace Alfa.CarRental.Application.Rentals.BookRental;

public record BookRentalCommand(
    Guid VehicleId,
    Guid Userid,
    DateOnly StartDate,
    DateOnly EndDate
    ) : ICommand<Guid>;    
