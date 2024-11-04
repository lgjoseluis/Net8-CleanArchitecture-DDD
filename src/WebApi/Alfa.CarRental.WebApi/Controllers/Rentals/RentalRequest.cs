namespace Alfa.CarRental.WebApi.Controllers.Rentals
{
    public sealed record RentalRequest(
        Guid VehicleId,
        Guid UserId,
        DateOnly StartDate,
        DateOnly EndDate
     );
}
