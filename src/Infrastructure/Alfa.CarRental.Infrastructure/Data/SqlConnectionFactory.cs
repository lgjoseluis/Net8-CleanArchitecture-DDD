using System.Data;

using Npgsql;

using Alfa.CarRental.Application.Abstractions.Data;

namespace Alfa.CarRental.Infrastructure.Data;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        IDbConnection connection = new NpgsqlConnection(_connectionString);

        connection.Open();

        return connection;
    }
}
