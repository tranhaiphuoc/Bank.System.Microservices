using System.Data;

namespace Bank.Transaction.Application.Abstraction.Data;

public interface IDataAccess
{
    string GetConnectionString();

    Task<IEnumerable<TResult>> LoadDataAsync<TResult, TParameter>(
      string sqlCommand,
      TParameter parameter,
      CommandType type = CommandType.StoredProcedure);

    Task<int> SaveDataAsync<TParameter>(
        string sqlCommand,
        TParameter parameter,
        CommandType type = CommandType.StoredProcedure);
}
