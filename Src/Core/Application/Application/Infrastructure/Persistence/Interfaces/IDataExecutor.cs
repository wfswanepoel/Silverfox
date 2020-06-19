using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Application.Infrastructure.Persistence.Interfaces
{
    public interface IDataExecutor
    {
        Task<IEnumerable<TModel>> ExecuteMany<TModel>(string storedProcedureName,
            Action<SqlCommand> modifyCommand = null) where TModel : new();

        Task<TModel> ExecuteSingle<TModel>(string storedProcedureName,
            Action<SqlCommand> modifyCommand = null) where TModel : new();

        Task<Tuple<IEnumerable<TModel>, IEnumerable<TModel2>>> ExecuteMany<TModel, TModel2>(string storedProcedureName,
            Action<SqlCommand> modifyCommand = null) where TModel : new();

        Task<TModel> ExecuteScalar<TModel>(string storedProcedureName, Action<SqlCommand> modifyCommand = null);

        Task<int> ExecuteRaw(string sql, Action<SqlCommand> modifyCommand = null);
    }
}
