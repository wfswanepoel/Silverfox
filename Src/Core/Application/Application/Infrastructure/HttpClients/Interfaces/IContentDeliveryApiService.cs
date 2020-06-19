using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Http.Clients.ContentDeliveryApi;

namespace Application.Infrastructure.HttpClients.Interfaces
{
    public interface IContentDeliveryApiService : IDisposable
    {
        Task<TModel> GetSingleContent<TModel>(ContentDeliveryApiSingleRequest request) where TModel : new();
    }
}
