using MediatR;
using Alfa.CarRental.Domain.Rentals.Events;
using Alfa.CarRental.Domain.Rentals;
using Alfa.CarRental.Domain.Users;
using Alfa.CarRental.Application.Abstractions.EMail;

namespace Alfa.CarRental.Application.Rentals.BookRental;

internal sealed class RentalReserveDomainEventHandler : INotificationHandler<RentalReservedDomainEvent>
{
    private readonly IRentalRepository _rentalRepository;

    private readonly IUserRepository _userRepository;

    private readonly IEmailService _emailService;

    public RentalReserveDomainEventHandler(IRentalRepository rentalRepository, IUserRepository userRepository, IEmailService emailService)
    {
        _rentalRepository = rentalRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(RentalReservedDomainEvent notification, CancellationToken cancellationToken)
    {
        Rental? rental = await _rentalRepository.FindByIdAsync(notification.rentalId, cancellationToken);

        if (rental is null)
        {
            return;
        }

        User? user = await _userRepository.FindByIdAsync(rental.UserId, cancellationToken);

        if (user is null)
        {
            return;
        }

        await _emailService.SendAsync(user.Email, "Reserva de alquiler", "Debe confirmar la reserva, de lo contraro se va a descartar");
    }
}
