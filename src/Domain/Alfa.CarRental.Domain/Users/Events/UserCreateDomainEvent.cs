using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Domain.Users.Events;

public sealed record UserCreateDomainEvent (Guid userId) : IDomainEvent;
