using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Domain.Vehicles;

public sealed class Vehicle : Entity
{
    private Vehicle( 
        Guid id,
        Model model,
        Serie serie,
        Address address,
        Currency price,
        Currency maintenanceCost,
        DateTime lastRentalDate,
        List<Accessory> accessories
        ) : base(id) 
    { 
        Model = model;
        Serie = serie;
        Address = address;
        Price = price;
        MaintenanceCost = maintenanceCost;
        LastRentalDate = lastRentalDate;
        Accessories = accessories;
    }

    public Model Model { get; private set; }

    public Serie Serie { get; private set; }

    public Address Address { get; private set; }    

    public Currency Price { get; private set; }

    public Currency MaintenanceCost {  get; private set; } 

    public DateTime LastRentalDate { get; private set; }

    public List<Accessory> Accessories { get; private set; }

    public static Vehicle Create(
        Model model,
        Serie serie,
        Address address,
        Currency price,
        Currency maintenanceCost,
        DateTime lastRentalDate,
        List<Accessory> accessories
        ) {

        Vehicle vehicle = new Vehicle(Guid.NewGuid(), model, serie, address, price, maintenanceCost, lastRentalDate, accessories);

        return vehicle;
    }
}
