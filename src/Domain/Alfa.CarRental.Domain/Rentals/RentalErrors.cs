using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Domain.Rentals;

public static class RentalErrors 
{
    public static Error NotFound = new Error("Rental.NotFound", "The rental with the id was not found");

    public static Error Overlap = new Error("Rental.Overlap", "The rental is being assigned to two clients");

    public static Error NotReserved = new Error("Rental.NotReserved", "The rental is not reserved");

    public static Error NotConfirmed = new Error("Rental.NotConfirmed", "The rental is not confirmed");

    public static Error AlreadyStarted = new Error("Rental.AlreadyStarted", "The rental is started");
}
