namespace Alfa.CarRental.Application.Rentals.GetRentalQuery;

public sealed class RentalResponse
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public Guid VehiculoId { get; init; }

    public int Status { get; init; }

    public decimal RentalAmount { get; init; }

    public string? RentalCurrencyType { get; set; }

    public decimal MaintenanceCost {  get; init; }

    public string? MaintenanceCurrencyType { get; init; }

    public decimal AccessoriesCost { get; init; }

    public string? AccessoriesCurrencyType { get; init; }

    public decimal TotalPrice { get; init; }

    public string? TotalCurrencyType { get; init; }

    public DateOnly StartDate { get; init; }

    public DateOnly EndDate { get; init; }

    public DateTime CreateDate { get; init; }
}