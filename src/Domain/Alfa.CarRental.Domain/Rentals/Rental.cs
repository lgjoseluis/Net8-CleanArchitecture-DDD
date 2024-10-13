using Alfa.CarRental.Domain.Abstractions;
using Alfa.CarRental.Domain.Rentals.Events;
using Alfa.CarRental.Domain.Vehicles;

namespace Alfa.CarRental.Domain.Rentals;

public sealed class Rental:Entity
{
    private Rental(Guid id,
        Guid vehicleId,
        Guid userId,
        DateRange dateSpan,
        Currency price,
        Currency maintenanceCost,
        Currency accessoriesCost,
        Currency totalPrice,
        RentalStatus status,
        DateTime createDate
        ) : base(id)
    {
        VehicleId = vehicleId;
        UserId = userId;
        DateSpan = dateSpan;
        Price = price;
        MaintenanceCost = maintenanceCost;
        AccessoriesCost = accessoriesCost;
        TotalPrice = totalPrice;
        Status = status;
        CreateDate = createDate;
    }

    public Guid VehicleId { get; private set; }

    public Guid UserId { get; private set; }

    public Currency Price { get; private set; }

    public Currency MaintenanceCost { get; private set; }

    public Currency AccessoriesCost { get; private set; }

    public RentalStatus Status { get; private set; }

    public DateRange DateSpan { get; private set; }

    public Currency TotalPrice {  get; private set; }

    public DateTime CreateDate { get; private set; }

    public DateTime ConfirmDate { get; private set; }

    public DateTime DenialDate { get; private set; }

    public DateTime CancelDate { get; private set; }

    public static Rental Reserve(
        Guid vehicleId,
        Guid userId,
        DateRange dateSpan,
        DateTime createDate,
        DetailPrice detailPrice)
    {
        Rental rental = new Rental(
            Guid.NewGuid(),
            vehicleId,
            userId,
            dateSpan,
            detailPrice.Price,
            detailPrice.MaintenanceCost,
            detailPrice.AccessoriesCost,
            detailPrice.TotalPrice,
            RentalStatus.Reserved,
            createDate
            );

        rental.RaiseDomainEvent(new RentalReservedDomainEvent(rental.Id));

        return rental;
    }
}
