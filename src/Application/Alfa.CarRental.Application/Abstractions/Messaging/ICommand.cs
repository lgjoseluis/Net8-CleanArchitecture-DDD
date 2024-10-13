using MediatR;

using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Application.Abstractions.Messaging;

public interface ICommand: IRequest<Result>, IBaseCommand
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
{
}

public interface IBaseCommand : IRequest<Result>
{
}
