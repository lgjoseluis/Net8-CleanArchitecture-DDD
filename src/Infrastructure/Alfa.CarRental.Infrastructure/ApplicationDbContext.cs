using Microsoft.EntityFrameworkCore;
using Alfa.CarRental.Domain.Abstractions;
using MediatR;

namespace Alfa.CarRental.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventAsync();

            return result;
        }
        catch (DbUpdateConcurrencyException ex) 
        {
            throw new DbUpdateConcurrencyException("A concurrency exception has been raised", ex);
        }
    }

    private async Task PublishDomainEventAsync() 
    {
        List<IDomainEvent> domainEvents = ChangeTracker.Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity => {
                IReadOnlyList<IDomainEvent> domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .ToList();
        
        foreach (IDomainEvent domainEvent in domainEvents) {
            await _publisher.Publish(domainEvent);
        }
    }
}
