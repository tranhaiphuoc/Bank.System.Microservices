using Bank.Customer.Application.Abstraction.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace Bank.Customer.Infrastructure.Data;

public class DataAccess(IConfiguration configuration) : IDataAccess
{
    private readonly string _connectionString = configuration.GetConnectionString("Database")!;

    public async Task<IEnumerable<TResult>> LoadDataAsync<TResult, TParameter>(
        string sqlCommand,
        TParameter parameter,
        CommandType type = CommandType.StoredProcedure)
    {
        using IDbConnection connection = new MySqlConnection(_connectionString);

        return await connection.QueryAsync<TResult>(sqlCommand, parameter, commandType: type);
    }

    public async Task<int> SaveDataAsync<TParameter>(
        string sqlCommand,
        TParameter parameter,
        CommandType type = CommandType.StoredProcedure)
    {
        using IDbConnection connection = new MySqlConnection(_connectionString);

        return await connection.ExecuteAsync(sqlCommand, parameter, commandType: type);
    }
}
