namespace Alfa.CarRental.Domain.Rentals;

public sealed record DateRange
{
    private DateRange(DateOnly startDate, DateOnly endDate) 
    { 
        StartDate = startDate;
        EndDate = endDate;
    }

    public DateOnly StartDate {  get; init; }

    public DateOnly EndDate { get; init; }

    public int Days => EndDate.DayNumber - StartDate.DayNumber;

    public static DateRange Create(DateOnly start, DateOnly end)
    { 
        if(start > end)
        {
            throw new ArgumentException("Incorrect date range");
        }

        return new DateRange(start, end);
    }
}
