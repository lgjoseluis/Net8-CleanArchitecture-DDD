namespace Alfa.CarRental.Domain.Users;

public interface IUserRepository
{
    Task AddAsync(User user);

    Task<User?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
