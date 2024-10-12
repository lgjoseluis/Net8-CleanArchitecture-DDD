using Alfa.CarRental.Domain.Abstractions;
using Alfa.CarRental.Domain.Users.Events;

namespace Alfa.CarRental.Domain.Users;

public sealed class User : Entity
{
    private User(
        Guid id,
        FirstName firstName,
        LastName lastName,
        Email email
        ) : base(id)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }

    public static User Create(FirstName firstName, LastName lastName, Email email)
    {
        User user = new User( Guid.NewGuid(), firstName, lastName, email );

        user.RaiseDomainEvent(new UserCreateDomainEvent(user.Id));

        return user;
    }
}
