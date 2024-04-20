using System.Data;

namespace Auth.Application.Abstraction.Data;

public interface IDataAccess
{
    Task<IEnumerable<T>> LoadDataAsync<T, U>(
        string sqlCommand,
        U parameter,
        CommandType type = CommandType.StoredProcedure,
        CancellationToken cancellationToken = default);

    Task<int> SaveDataAsync<T>(
        string sqlCommand,
        T parameter,
        CommandType type = CommandType.StoredProcedure,
        CancellationToken cancellationToken = default);
}
