using System;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Http.Enums;
using Domain.Http.Models;

namespace Application.Infrastructure.HttpClients.Interfaces
{
    public interface IHttpClientExecutorFactory : IDisposable
    {
        Task<HttpResponseMessage> ExecuteGetAsync(HttpClientConnections httpClientConnection, string url,
            HttpClientExecutorExtras httpClientExecutorExtras);

        Task<HttpResponseMessage> ExecutePostAsync<TModel>(HttpClientConnections httpClientConnection, string url,
            TModel data, HttpClientExecutorExtras httpClientExecutorExtras) where TModel : new();
    }
}
