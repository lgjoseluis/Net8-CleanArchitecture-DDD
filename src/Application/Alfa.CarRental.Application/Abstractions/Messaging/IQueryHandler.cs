using Alfa.CarRental.Domain.Abstractions;
using MediatR;

namespace Alfa.CarRental.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> 
    : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
{
}
