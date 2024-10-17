using Microsoft.EntityFrameworkCore;

using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Infrastructure.Repositories;

internal abstract class Repository<T> where T : Entity
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<T?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbContext
            .Set<T>()
            .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);
    }

    public async Task AddAsync(T entity)
    { 
        await DbContext.Set<T>().AddAsync(entity);
    }
}
