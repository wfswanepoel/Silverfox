using System;
using System.Collections.Generic;
using System.Text;
using Application.Infrastructure.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace Application.Products.Queries.GetProduct
{
    public class GetProductQueryCache : DistributedCache<GetProductQuery, ProductResponse>
    {
        public GetProductQueryCache(IDistributedCache distributedCache, IConfiguration configuration) : base(distributedCache, configuration)
        {
        }

        protected override string GetCacheKeyIdentifier(GetProductQuery request)
        {
            return $"Product-{request.CultureName}-{request.Id}-{request.Date.Date:yyyy-MM-dd}";
        }
    }
}
