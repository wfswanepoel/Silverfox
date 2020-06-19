using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using AutoMapper.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace DataExecutor.Models
{
    public class DataExecutorCustom : DataExecutor
    {
        public DataExecutorCustom(IConfiguration configuration, IMapper mapper) : base(mapper)
        {
            ConnectionString = configuration["connectionStrings:custom:ConnectionString"];
        }
    }
}
