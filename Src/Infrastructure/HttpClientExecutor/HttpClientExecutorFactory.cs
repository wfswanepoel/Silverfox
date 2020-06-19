using System;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Infrastructure.HttpClients.Interfaces;
using Domain.Http.Enums;
using Domain.Http.Models;

namespace HttpClientExecutor
{
    public class HttpClientExecutorFactory : IHttpClientExecutorFactory
    {
        public Task<HttpResponseMessage> ExecuteGetAsync(HttpClientConnections httpClientConnection, string url,
            HttpClientExecutorExtras httpClientExecutorExtras)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseMessage> ExecutePostAsync<TModel>(HttpClientConnections httpClientConnection, string url, TModel data,
            HttpClientExecutorExtras httpClientExecutorExtras) where TModel : new()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
