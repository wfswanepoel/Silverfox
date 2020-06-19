using System;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Http.Models;

namespace Application.Infrastructure.HttpClients.Interfaces
{
    public interface IHttpClientExecutor : IDisposable
    {
        HttpClientExternalOptions HttpClientExecutorOptions { get; set; }
        Task<HttpResponseMessage> ExecuteGetAsync(string url, HttpClientExecutorExtras httpClientExecutorExtras);

        Task<HttpResponseMessage> ExecutePostAsync<TModel>(string url, TModel data, HttpClientExecutorExtras httpClientExecutorExtras)
            where TModel : new();
    }
}
