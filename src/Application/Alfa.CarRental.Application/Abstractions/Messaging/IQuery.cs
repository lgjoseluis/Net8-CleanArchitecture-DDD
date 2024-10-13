using MediatR;

using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
