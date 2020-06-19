using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Application.Infrastructure.Persistence.Interfaces;
using AutoMapper;
using DataExecutor.Models;
using Domain.Persistence.Enums;
using Microsoft.Extensions.Configuration;

namespace DataExecutor
{
    public class DataExecutorFactory : IDataExecutorFactory
    {
        private readonly Dictionary<PersistenceDatabases, DataExecutor> _factories;

        public DataExecutorFactory(IConfiguration configuration, IMapper mapper)
        {
            _factories = new Dictionary<PersistenceDatabases, DataExecutor>
            {
                {PersistenceDatabases.Cms, new DataExecutorCms(configuration, mapper)},
                {PersistenceDatabases.Commerce, new DataExecutorCommerce(configuration, mapper)},
                {PersistenceDatabases.Custom, new DataExecutorCustom(configuration, mapper)}
            };
        }
        
        public async Task<IEnumerable<TModel>> ExecuteMany<TModel>(PersistenceDatabases database, string storedProcedure, Action<SqlCommand> command = null) where TModel : new()
        {
            return await _factories[database].ExecuteMany<TModel>(storedProcedure, command);
        }

        public async Task<Tuple<IEnumerable<TModel>, IEnumerable<TModel2>>> ExecuteMany<TModel, TModel2>(PersistenceDatabases database, string storedProcedureName,
            Action<SqlCommand> modifyCommand = null) where TModel : new()
        {
            return await _factories[database].ExecuteMany<TModel, TModel2>(storedProcedureName, modifyCommand);
        }

        public async Task<TModel> ExecuteSingle<TModel>(PersistenceDatabases database, string storedProcedure, Action<SqlCommand> command = null) where TModel : new()
        {
            return await _factories[database].ExecuteSingle<TModel>(storedProcedure, command);
        }

        public async Task<TModel> ExecuteScalar<TModel>(PersistenceDatabases database, string storedProcedure, Action<SqlCommand> command = null)
        {
            return await _factories[database].ExecuteScalar<TModel>(storedProcedure, command);
        }

        public async Task ExecuteRaw(PersistenceDatabases database, string sql, Action<SqlCommand> command = null)
        {
            await _factories[database].ExecuteRaw(sql, command);
        }

        public void Dispose()
        {
        }
    }
}
