using Alfa.CarRental.Application.Abstractions.Messaging;

namespace Alfa.CarRental.Application.Rentals.GetRentalQuery;

public sealed record GetRentalQuery(Guid RentalId) : IQuery<RentalResponse>;

