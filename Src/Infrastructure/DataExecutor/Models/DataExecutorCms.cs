using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace DataExecutor.Models
{
    public class DataExecutorCms : DataExecutor
    {
        public DataExecutorCms(IConfiguration configuration, IMapper mapper) : base(mapper)
        {
            ConnectionString = configuration["connectionStrings:cms:ConnectionString"];
        }
    }
}
