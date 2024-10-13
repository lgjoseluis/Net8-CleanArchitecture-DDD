using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Domain.Rentals;

public static class RentalErrors 
{
    public static readonly Error NotFound = new ("Rental.NotFound", "The rental with the id was not found");

    public static readonly Error Overlap = new ("Rental.Overlap", "The rental is being assigned to two clients");

    public static readonly Error NotReserved = new ("Rental.NotReserved", "The rental is not reserved");

    public static readonly Error NotConfirmed = new ("Rental.NotConfirmed", "The rental is not confirmed");

    public static readonly Error AlreadyStarted = new ("Rental.AlreadyStarted", "The rental is started");
}
