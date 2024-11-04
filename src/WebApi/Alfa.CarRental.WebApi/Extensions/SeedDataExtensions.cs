using System.Data;

using Alfa.CarRental.Application.Abstractions.Data;
using Alfa.CarRental.Domain.Vehicles;
using Bogus;
using Dapper;

namespace Alfa.CarRental.WebApi.Extensions
{
    public static class SeedDataExtensions
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            ISqlConnectionFactory sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
            using IDbConnection connection = sqlConnectionFactory.CreateConnection();

            Faker faker = new Faker();

            List<object> vehicles = new();

            for (int i = 0; i < 100; i++)
            {
                vehicles.Add(new
                {
                    Id = Guid.NewGuid(),
                    Model = faker.Vehicle.Model(),
                    Serie = faker.Vehicle.Vin(),                    
                    Country = faker.Address.Country(),
                    City = faker.Address.State(),
                    Province = faker.Address.City(),
                    Department = faker.Address.County(),
                    Street = faker.Address.StreetAddress(),
                    Price = faker.Random.Decimal(1000, 20000),
                    CurrencyPrice = "USD",
                    MaintenanceCost = faker.Random.Decimal(100, 200),
                    CurrencyMaintenanceCost =  "USD",
                    LastRentalDate = DateTime.MinValue,
                    Accessories = new List<int>() { (int)Accessory.Wifi, (int)Accessory.Maps }                    
                });
            }

            const string sql = """
                INSERT INTO public.vehicles(
                        id, model, serie, address_country, address_city, address_province, address_department, address_street, price_amount, 
                        price_currency_type, maintenance_cost_amount, maintenance_cost_currency_type, last_rental_date, accessories
                    )
                    VALUES(
                        @Id, @Model, @Serie, @Country, @City, @Province, @Department, @Street, @Price, 
                        @CurrencyPrice, @MaintenanceCost, @CurrencyMaintenanceCost, @LastRentalDate, @Accessories
                    )

                """;

            connection.Execute(sql, vehicles);
        }
    }
}
