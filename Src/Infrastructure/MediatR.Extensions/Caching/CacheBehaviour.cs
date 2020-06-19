using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Caching.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MediatR.Extensions.Caching
{
    public class CacheBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly List<ICache<TRequest, TResponse>> _caches;
        private readonly IConfiguration _configuration;

        public CacheBehaviour(IEnumerable<ICache<TRequest, TResponse>> cachedRequests, IConfiguration configuration)
        {
            _configuration = configuration;
            _caches = cachedRequests?.ToList();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            bool.TryParse(_configuration["EnableCaching"], out var shouldCache);

            if (!shouldCache || _caches == null)
                return await next();

            var cacheRequest = _caches.FirstOrDefault();
            if (cacheRequest == null)
            {
                return await next();
            }

            var cachedResult = await cacheRequest.Get(request);

            if (cachedResult != null)
            {
                return cachedResult;
            }

            var result = await next();
            await cacheRequest.Set(request, result);
            return result;
        }
    }
}
