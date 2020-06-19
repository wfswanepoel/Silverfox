using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Domain.Persistence.Enums;

namespace Application.Infrastructure.Persistence.Interfaces
{
    public interface IDataExecutorFactory : IDisposable
    {
        Task<IEnumerable<TModel>> ExecuteMany<TModel>(PersistenceDatabases database, string storedProcedure,
            Action<SqlCommand> command = null) where TModel : new();
        Task<Tuple<IEnumerable<TModel>, IEnumerable<TModel2>>> ExecuteMany<TModel, TModel2>(PersistenceDatabases database, string storedProcedureName,
            Action<SqlCommand> modifyCommand = null) where TModel : new();
        Task<TModel> ExecuteSingle<TModel>(PersistenceDatabases database, string storedProcedure,
            Action<SqlCommand> command = null) where TModel : new();
        Task<TModel> ExecuteScalar<TModel>(PersistenceDatabases database, string storedProcedure, Action<SqlCommand> command = null);
        Task ExecuteRaw(PersistenceDatabases database, string sql, Action<SqlCommand> command = null);
    }
}
