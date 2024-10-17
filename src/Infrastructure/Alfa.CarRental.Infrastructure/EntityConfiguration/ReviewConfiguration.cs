using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Alfa.CarRental.Domain.Reviews;
using Alfa.CarRental.Domain.Vehicles;
using Alfa.CarRental.Domain.Rentals;
using Alfa.CarRental.Domain.Users;

namespace Alfa.CarRental.Infrastructure.EntityConfiguration;

internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews").HasKey(x => x.Id);

        builder.Property(review => review.Rating)
            .HasConversion(rating => rating.Value, value=> Rating.Create(value).Value);

        builder.Property(review => review.Comment)
            .HasMaxLength(200)
            .HasConversion(comment => comment.Value, value => new Comment(value));

        builder.HasOne<Vehicle>()
            .WithMany()
            .HasForeignKey(review => review.VehicleId);

        builder.HasOne<Rental>()
            .WithMany()
            .HasForeignKey(review => review.RentalId);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(review => review.UserId);
    }
}
