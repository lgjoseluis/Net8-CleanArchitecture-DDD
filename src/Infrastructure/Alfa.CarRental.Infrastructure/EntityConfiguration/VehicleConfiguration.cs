using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Alfa.CarRental.Domain.Vehicles;
using Alfa.CarRental.Domain.Shared;

namespace Alfa.CarRental.Infrastructure.EntityConfiguration;

internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles");

        builder.HasKey(k => k.Id);

        builder.OwnsOne(vehicle => vehicle.Address);

        builder.Property(vehicle => vehicle.Model)
            .HasMaxLength(200)
            .HasConversion(model => model.Value, value => new Model(value));

        builder.Property(vehicle => vehicle.Serie)
            .HasMaxLength(500)
            .HasConversion(serie => serie.Value, value => new Serie(value));

        builder.OwnsOne(vehicle => vehicle.Price, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
                .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });

        builder.OwnsOne(vehicle => vehicle.MaintenanceCost, priceBuilder => {
            priceBuilder.Property(currency => currency.CurrencyType)
                .HasConversion(currencyType => currencyType.Code, code => CurrencyType.FromCode(code!));
        });
    }
}
