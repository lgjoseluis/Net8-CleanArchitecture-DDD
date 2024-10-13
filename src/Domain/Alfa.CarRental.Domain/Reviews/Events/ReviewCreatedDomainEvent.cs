using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Domain.Reviews.Events;

public sealed record ReviewCreatedDomainEvent(Guid reviewId):IDomainEvent;

