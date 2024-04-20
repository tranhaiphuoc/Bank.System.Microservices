using System.Data;

namespace Bank.Account.Application.Abstraction.Data;

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
