using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Alfa.CarRental.Domain.Rentals;
using Alfa.CarRental.Domain.Shared;
using Alfa.CarRental.Domain.Vehicles;
using Alfa.CarRental.Domain.Users;

namespace Alfa.CarRental.Infrastructure.EntityConfiguration;

internal sealed class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ToTable("rentals")
            .HasKey(x => x.Id);

        builder.OwnsOne(rental => rental.Price, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(rental => rental.MaintenanceCost, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(rental => rental.AccessoriesCost, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(rental => rental.TotalPrice, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
            .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(rental => rental.DateRange);

        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(rental => rental.VehicleId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(rental => rental.UserId);
    }
}
