using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfa.CarRental.Domain.Vehicles
{
    public record Currency (decimal Amount, CurrencyType CurrencyType)
    {
        public static Currency operator +(Currency first, Currency second) 
        { 
            if(first.CurrencyType != second.CurrencyType) 
                throw new InvalidCastException("The type of currency must be the same");

            return new Currency(first.Amount + second.Amount, first.CurrencyType);
        }

        public static Currency Zero() => new Currency(0, CurrencyType.NONE);

        public static Currency Zero(CurrencyType currencyType) => new Currency(0, currencyType);

        public bool IsZero => this == Zero(CurrencyType);
    }
}
