using System;
using System.Threading.Tasks;
using Application.Infrastructure.Caching.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;

namespace Application.Infrastructure.Caching
{
    public abstract class DistributedCache<TRequest, TResponse> : ICache<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IDistributedCache _distributedCache;
        protected virtual DateTime? AbsoluteExpiration { get; }
        protected virtual TimeSpan? AbsoluteExpirationRelativeToNow { get; }
        protected virtual TimeSpan? SlidingExpiration { get; }

        protected DistributedCache(
            IDistributedCache distributedCache, IConfiguration configuration)
        {
            _distributedCache = distributedCache;
            AbsoluteExpiration = DateTime.Now.AddMinutes(Convert.ToInt32(configuration["AbsoluteExpirationCacheMinutes"]));
            SlidingExpiration = TimeSpan.FromMinutes(Convert.ToInt32(configuration["SlidingExpirationCacheMinutes"]));
        }

        protected abstract string GetCacheKeyIdentifier(TRequest request);

        private static string GetCacheKey(string id)
        {
            return id;
        }

        private string GetCacheKey(TRequest request)
        {
            return GetCacheKey(GetCacheKeyIdentifier(request));
        }

        public virtual async Task<TResponse> Get(TRequest request)
        {
            var response = await _distributedCache.GetAsync<TResponse>(GetCacheKey(request));
            return response == null ? default(TResponse) : response;
        }

        public virtual async Task Set(TRequest request, TResponse value)
        {
            if (value == null)
                return;

            await _distributedCache.SetAsync(
                GetCacheKey(request),
                value,
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = AbsoluteExpiration,
                    AbsoluteExpirationRelativeToNow = AbsoluteExpirationRelativeToNow,
                    SlidingExpiration = SlidingExpiration
                });
        }

        public async Task Remove(string cacheKeyIdentifier)
        {
            await _distributedCache.RemoveAsync(GetCacheKey(cacheKeyIdentifier));
        }
    }
}
