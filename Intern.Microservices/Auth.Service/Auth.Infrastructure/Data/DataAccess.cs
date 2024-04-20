using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using Auth.Application.Abstraction.Data;

namespace Auth.Infrastructure.Data;

public class DataAccess(IConfiguration configuration) : IDataAccess
{
    private readonly string _connectionString = configuration.GetConnectionString("Default")!;

    public async Task<IEnumerable<T>> LoadDataAsync<T, U>(
        string sqlCommand,
        U parameter,
        CommandType type = CommandType.StoredProcedure,
        CancellationToken cancellationToken = default)
    {
        using IDbConnection connection = new MySqlConnection(_connectionString);

        return await connection.QueryAsync<T>(
            new CommandDefinition(
                sqlCommand,
                parameter,
                commandType: type,
                cancellationToken: cancellationToken));
    }

    public async Task<int> SaveDataAsync<T>(
        string sqlCommand,
        T parameter,
        CommandType type = CommandType.StoredProcedure,
        CancellationToken cancellationToken = default)
    {
        using IDbConnection connection = new MySqlConnection(_connectionString);

        return await connection.ExecuteAsync(
             new CommandDefinition(
                 sqlCommand,
                 parameter,
                 commandType: type,
                 cancellationToken: cancellationToken));
    }
}
