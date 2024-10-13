using Alfa.CarRental.Domain.Abstractions;

namespace Alfa.CarRental.Domain.Users;

public static class UserErrors
{
    public static readonly Error NotFound = new("User.NotFound", "User not found");
    public static readonly Error InvalidCredentials = new("User.InvalidCredentials", "User data is incorrect");
}
