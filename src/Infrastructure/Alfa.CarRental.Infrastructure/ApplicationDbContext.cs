using Microsoft.EntityFrameworkCore;
using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options) 
    {        
    }
}
