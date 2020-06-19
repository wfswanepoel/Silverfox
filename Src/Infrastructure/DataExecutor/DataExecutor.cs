using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Application.Infrastructure.Persistence.Interfaces;
using AutoMapper;

namespace DataExecutor
{
    public class DataExecutor : IDataExecutor
    {
        protected string ConnectionString;
        private readonly IMapper _mapper;
        
        public DataExecutor(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IEnumerable<TModel>> ExecuteMany<TModel>(string storedProcedureName, Action<SqlCommand> modifyCommand = null) where TModel : new()
        {
            var result = await GetConnectionMany<TModel>(async connection =>
            {
                using (var command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    modifyCommand?.Invoke(command);

                    using (var sqlReader = await command.ExecuteReaderAsync())
                    {
                        var items = new List<TModel>();

                        while (sqlReader.Read())
                            items.Add(_mapper.Map<IDataRecord, TModel>(sqlReader));

                        return items;
                    }
                }
            });

            return result;
        }

        public async Task<TModel> ExecuteSingle<TModel>(string storedProcedureName, Action<SqlCommand> modifyCommand = null) where TModel : new()
        {
            TModel result = default;

            await GetConnection<TModel>(async connection =>
            {
                using (var command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    modifyCommand?.Invoke(command);

                    using (var sqlReader = await command.ExecuteReaderAsync())
                    {
                        while (sqlReader.Read())
                            result = _mapper.Map<IDataRecord, TModel>(sqlReader);
                    }

                    return result;
                }
            });

            return result;
        }

        public async Task<Tuple<IEnumerable<TModel>, IEnumerable<TModel2>>> ExecuteMany<TModel, TModel2>(string storedProcedureName, Action<SqlCommand> modifyCommand = null) where TModel : new()
        {
            Tuple<IEnumerable<TModel>, IEnumerable<TModel2>> result = null;

            await GetSqlDataReader(storedProcedureName, reader => {

                var list = _mapper.Map<IDataRecord, IList<TModel>>(reader);
                reader.NextResult();
                var list2 = _mapper.Map<IDataRecord, IList<TModel2>>(reader);
                result = new Tuple<IEnumerable<TModel>, IEnumerable<TModel2>>(list, list2);

            }, modifyCommand);

            return result;
        }

        public async Task<TModel> ExecuteScalar<TModel>(string storedProcedureName, Action<SqlCommand> modifyCommand = null)
        {
            TModel result = default;

            await GetSqlDataReader(storedProcedureName, reader =>
            {
                if (reader.Read())
                    result = (TModel)reader.GetValue(0);

            }, modifyCommand);

            return result;
        }

        public async Task<int> ExecuteRaw(string sql, Action<SqlCommand> modifyCommand = null)
        {
            return await GetConnection<int>(async connection =>
            {
                using (var command = new SqlCommand(sql, connection))
                {
                    modifyCommand?.Invoke(command);
                    return await command.ExecuteNonQueryAsync();
                }
            });
        }
        
        private async Task GetSqlDataReader(string storedProcedureName, Action<SqlDataReader> read, Action<SqlCommand> modifyCommand = null)
        {
            await GetConnection(async connection =>
            {
                using (var command = new SqlCommand(storedProcedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    modifyCommand?.Invoke(command);

                    using (var sqlReader = await command.ExecuteReaderAsync())
                    {
                        read(sqlReader);
                    }
                }
            });
        }

        private async Task GetConnection(Func<SqlConnection, Task> doAction)
        {
            await GetConnection<object>(async connection =>
            {
                await doAction(connection);
                return null;
            });
        }

        private async Task<T> GetConnection<T>(Func<SqlConnection, Task<T>> doAction)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                return await doAction(connection);
            }
        }

        private async Task<IEnumerable<T>> GetConnectionMany<T>(Func<SqlConnection, Task<IEnumerable<T>>> doAction)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.OpenAsync();
                return await doAction(connection);
            }
        }
    }
}
