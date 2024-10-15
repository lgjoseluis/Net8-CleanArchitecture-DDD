using System.Data;

using Alfa.CarRental.Application.Abstractions.Data;
using Alfa.CarRental.Application.Abstractions.Messaging;
using Alfa.CarRental.Domain.Abstractions;
using Dapper;

namespace Alfa.CarRental.Application.Rentals.GetRentalQuery;

internal sealed class GetRentalQueryHandler : IQueryHandler<GetRentalQuery, RentalResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetRentalQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _sqlConnectionFactory = connectionFactory;
    }

    public async Task<Result<RentalResponse>> Handle(GetRentalQuery request, CancellationToken cancellationToken)
    {
        using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
        const string query = """ 
            SELECT
                id as Id,
                vehicle_id as VehicleId,
                user_id as UserId,
                status as Status,
                rental_amount as RentalAmount,
                rental_currency_type as RentalCurrencyType,
                maintenance_cost as MaintenanceCost,
                maintenance_currency_type as MaintenanceCurrencyType,
                accessories_cost as AccessoriesCost,
                accessories_currency_type as AccessoriesCurrencyType,
                total_price as TotalPrice,
                total_currency_type as TotalCurrencyType,
                start_date as StartDate,
                end_date as EndDate,
                create_date as CreateDate
            FROM rentals
            WHERE Id = @RentalId
            """;

        RentalResponse? rentalResponse = await connection.QueryFirstOrDefaultAsync<RentalResponse>(query, new { request.RentalId });

        return rentalResponse;
    }
}
