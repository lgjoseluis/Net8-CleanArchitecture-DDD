using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Domain.Reviews;

public sealed record Rating//(int value);    
{
    public static Error Invalid = new("Rating.Invalid", "Rating invalid");

    public int Value {  get; init; }

    private Rating(int value)
    {  
        Value = value; 
    }

    public static Result<Rating> Create(int value)
    {
        if (value < 1 || value > 5)
        {
            return Result.Failure<Rating>(Invalid);
        }

        Rating rating = new(value);

        return rating;
    }
}