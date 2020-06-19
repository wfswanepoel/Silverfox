using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Application.Infrastructure.HttpClients.Interfaces;
using Domain.Http.Enums;
using Domain.Http.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HttpClientExecutor
{
    public abstract class HttpClientExecutor : IHttpClientExecutor
    {
        private readonly IConfiguration _configuration;
        protected HttpClient ExecuterHttpClient;

        protected HttpClientExecutor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HttpClientExternalOptions HttpClientExecutorOptions { get; set; }

        protected virtual HttpClientExternalOptions GetConfigurations(HttpClientConnections connectionName)
        {
            return new HttpClientExternalOptions
            {
                GrandType = _configuration[$"{connectionName}:GrandType"],
                UserName = _configuration[$"{connectionName}:UserName"],
                Password = _configuration[$"{connectionName}:Password"],
                AuthenticateRequestUri = _configuration[$"{connectionName}:AuthenticateRequestUri"],
                BaseAddress = _configuration[$"{connectionName}:BaseAddress"],
                UseAuthorizationRequest = bool.Parse(_configuration[$"{connectionName}:UseAuthorizationRequest"]),
                HttpClientConnection = connectionName
            };
        }

        public async Task<HttpResponseMessage> ExecuteGetAsync(string url, HttpClientExecutorExtras httpClientExecutorExtras)
        {
            ExecuterHttpClient = CreateExecuterHttpClient();

            if (HttpClientExecutorOptions != null && HttpClientExecutorOptions.UseAuthorizationRequest)
            {
                var authenticateResponse = await Authenticate();
                AddAuthorizationTokenToClientHeader(authenticateResponse);
            }

            AddHeadersToClient(ExecuterHttpClient, httpClientExecutorExtras.Headers);

            return await ExecuterHttpClient.GetAsync(url, httpClientExecutorExtras.CancellationToken);
        }

        public virtual async Task<HttpResponseMessage> ExecutePostAsync<TModel>(string url, TModel data, HttpClientExecutorExtras httpClientExecutorExtras) where TModel : new()
        {
            ExecuterHttpClient = CreateExecuterHttpClient();

            if (HttpClientExecutorOptions != null && HttpClientExecutorOptions.UseAuthorizationRequest)
            {
                var authenticateResponse = await Authenticate();
                AddAuthorizationTokenToClientHeader(authenticateResponse);
            }

            var content = JsonConvert.SerializeObject(data);
            var requestBody = new StringContent(content, Encoding.UTF8, "application/json");
            AddHeadersToClient(ExecuterHttpClient, httpClientExecutorExtras.Headers);

            return await ExecuterHttpClient.PostAsync(url, requestBody, httpClientExecutorExtras.CancellationToken);
        }
        protected abstract void AddAuthorizationTokenToClientHeader(IAuthenticateResponse authenticateResponse);
        protected abstract Task<IAuthenticateResponse> Authenticate();
        protected abstract HttpClient CreateExecuterHttpClient();
        private static void AddHeadersToClient(HttpClient httpClient, Dictionary<string, string> headers)
        {
            if (headers == null) return;

            foreach (var (key, value) in headers)
            {
                httpClient.DefaultRequestHeaders.Add(key, value);
            }
        }
        public void Dispose()
        {
            ExecuterHttpClient?.Dispose();
        }
    }
}
