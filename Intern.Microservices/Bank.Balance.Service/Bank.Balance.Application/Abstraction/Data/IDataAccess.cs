using System.Data;

namespace Bank.Balance.Application.Abstraction.Data;

public interface IDataAccess
{
    Task<IEnumerable<TResult>> LoadDataAsync<TResult, TParameter>(
        string sqlCommand,
        TParameter parameter,
        CommandType type = CommandType.StoredProcedure);

    Task<int> SaveDataAsync<TParameter>(
        string sqlCommand,
        TParameter parameter,
        CommandType type = CommandType.StoredProcedure);
}
