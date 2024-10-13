using Alfa.CarRental.Domain.Users;

namespace Alfa.CarRental.Application.Abstractions.EMail;

public interface IEmailService
{
    Task SendAsync(Email recipient, string subject, string body);
}
