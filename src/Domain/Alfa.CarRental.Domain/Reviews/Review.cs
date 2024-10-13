using Alfa.CarRental.Domain.Abstractions;
using Alfa.CarRental.Domain.Rentals;
using Alfa.CarRental.Domain.Reviews.Events;

namespace Alfa.CarRental.Domain.Reviews;

public sealed class Review : Entity
{
    public Guid VehicleId { get; private set; }

    public Guid RentalId { get; private set; }

    public Guid UserId { get; private set; }

    public Rating Rating { get; private set; }

    public Comment Comment { get; private set; }

    public DateTime CreateDate { get; private set; }

    private Review(
        Guid id,
        Guid vehicleId,
        Guid rentalId,
        Guid userId,
        Rating rating,
        Comment comment,
        DateTime createDate
        ) : base(id)
    {
        VehicleId = vehicleId;
        RentalId = rentalId;
        UserId = userId;
        Rating = rating;
        Comment = comment;
        CreateDate = createDate;
    }

    public static Result<Review> Create(
        Rental rental,
        Rating rating,
        Comment comment,
        DateTime createDate
        ) 
    {
        if (rental.Status != RentalStatus.Completed)
        {
            return Result.Failure<Review>(ReviewErrors.NotEligible);
        }

        Review review = new Review(
            Guid.NewGuid(),
            rental.VehicleId,
            rental.Id,
            rental.UserId,
            rating,
            comment,
            createDate
            );

        review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));

        return review;
    }
}
