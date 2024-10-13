namespace Alfa.CarRental.Domain.Users;

public interface IUserRepository
{
    Task AddAsync(User user);

    Task<User?> FindAsync(Guid id, CancellationToken cancellationToken = default);
}
