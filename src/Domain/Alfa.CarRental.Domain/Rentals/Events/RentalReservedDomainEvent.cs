using Alfa.CarRental.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfa.CarRental.Domain.Rentals.Events
{
    public sealed record RentalReservedDomainEvent(Guid rentalId):IDomainEvent;
    
}
