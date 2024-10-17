using Alfa.CarRental.Application.Abstractions.EMail;
using Alfa.CarRental.Domain.Users;

namespace Alfa.CarRental.Infrastructure.EMail;

internal sealed class EmailService : IEmailService
{
    public async Task SendAsync(Email recipient, string subject, string body)
    {
        throw new NotImplementedException();
    }
}
