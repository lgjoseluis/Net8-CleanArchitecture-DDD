using Alfa.CarRental.Application.Abstractions.Data;
using Alfa.CarRental.Application.Abstractions.Messaging;
using Alfa.CarRental.Domain.Abstractions;
using Alfa.CarRental.Domain.Rentals;
using Dapper;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;

namespace Alfa.CarRental.Application.Vehicles.SearchVehicles;

internal sealed class SearchVehiclesQueryHandler : IQueryHandler<SearchVehiclesQuery, IReadOnlyList<VehicleResponse>>
{
    private static readonly int[] ActiveRentalStatuses = { 
        (int)RentalStatus.Reserved,
        (int)RentalStatus.Confirmed,
        (int)RentalStatus.Completed
    };

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public SearchVehiclesQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<IReadOnlyList<VehicleResponse>>> Handle(SearchVehiclesQuery request, CancellationToken cancellationToken)
    {
        if(request.StartDate>request.EndDate)
        {
            return ReadOnlyCollection<VehicleResponse>.Empty;
        }

        const string query = """
            SELECT
                a.id as Id,
                a.model as Model,
                a.serie as Serie,
                a.price as Price,
                a.currency_type as CurrencyType,
                a.adresss_country as Country,
                a.address_city as City,
                a.address_province as Province,
                a.address_department as Department,
                a.address_street as Street
            FROM vehiclesas a
            WHERE NOT EXISTS
            (
                SELECT 1
                FROM rentals AS b
                WHERE b.vehicle_id = a.id
                    AND b.start_date <= @EndDate
                    AND b.end_date >= @StartDate
                    abd b.status = ANY(@ActiveRentalStatuses)
            )
            """;

        using var connection = _sqlConnectionFactory.CreateConnection();

        IEnumerable<VehicleResponse> vehicles = await connection.QueryAsync<VehicleResponse, AddressResponse, VehicleResponse>(
            query, 
            (vehicle, address) => {
                vehicle.Address = address;
                return vehicle;
            },
            new { 
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ActiveRentalStatuses
            },
            splitOn : "Country"
            );

        return vehicles.ToList();
    }
}