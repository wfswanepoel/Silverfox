using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.Caching.Interfaces;

namespace MediatR.Extensions.Caching
{
    public class CacheInvalidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly List<ICacheInvalidator<TRequest>> _cacheInvalidators;
        public CacheInvalidationBehavior(IEnumerable<ICacheInvalidator<TRequest>> cacheInvalidators)
        {
            _cacheInvalidators = cacheInvalidators?.ToList();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var result = await next();

            if (_cacheInvalidators == null)
                return result;

            foreach (var invalidator in _cacheInvalidators)
            {
                await invalidator.Invalidate(request);
            }

            return result;
        }
    }
}
