using Alfa.CarRental.Domain.Vehicles;

namespace Alfa.CarRental.Domain.Rentals;

public class PriceService
{
    public DetailPrice Caulculate(Vehicle vehicle, DateRange dateRange)
    {
        CurrencyType currencyType = vehicle.Price.CurrencyType;

        Currency price = new Currency(
            dateRange.Days * vehicle.Price.Amount,
            currencyType
            );

        decimal percentageCharge = 0;

        foreach (Accessory accessory in vehicle.Accessories)
        {
            percentageCharge += accessory switch 
            { 
                Accessory.AppleCar or Accessory.AndroidCar => 0.5m,
                Accessory.AirConditioning => 0.01m,
                Accessory.Maps => 0.01m,
                _ => 0m
            };
        }

        Currency charges= Currency.Zero(currencyType);

        if (percentageCharge > 0)
        {
            charges = new Currency(
                price.Amount*percentageCharge,
                currencyType
                );
        }

        Currency total = Currency.Zero();

        total += price;

        total += charges;

        if (!vehicle!.MaintenanceCost.IsZero)
        { 
            total += vehicle.MaintenanceCost;
        }

        return new DetailPrice(
            price, 
            vehicle.MaintenanceCost, 
            charges, 
            total);
    }
}
