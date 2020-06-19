using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Infrastructure.Caching.Interfaces
{
    public interface ICache<in TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Get(TRequest request);
        Task Set(TRequest request, TResponse value);
        Task Remove(string cacheKeyIdentifier);
    }
}
