using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Domain.Reviews;

public static class ReviewErrors
{
    public static readonly Error NotEligible = new("Review.NotEligible", "The rental has not yet been completed, a rating cannot be made");
}
