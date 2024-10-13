using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Domain.Rentals.Events;

public sealed record RentalCompletedDomainEvent(Guid rentalId):IDomainEvent;
