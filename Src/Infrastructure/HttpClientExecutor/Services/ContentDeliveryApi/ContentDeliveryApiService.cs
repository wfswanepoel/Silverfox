using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Infrastructure.HttpClients.Interfaces;
using Domain.Http.Clients.ContentDeliveryApi;
using Domain.Http.Enums;
using Domain.Http.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HttpClientExecutor.Services.ContentDeliveryApi
{
    public class ContentDeliveryApiService : IContentDeliveryApiService
    {
        private readonly IHttpClientExecutorFactory _httpClientExecutorFactory;
        private readonly IConfiguration _configuration;

        public ContentDeliveryApiService(IHttpClientExecutorFactory httpClientExecutorFactory, IConfiguration configuration)
        {
            _httpClientExecutorFactory = httpClientExecutorFactory;
            _configuration = configuration;
        }

        public async Task<TModel> GetSingleContent<TModel>(ContentDeliveryApiSingleRequest request) where TModel : new()
        {
            var queryString = string.Empty;

            if (request.ContentId != default)
                queryString = $"{request.ContentId}";

            var responseContent = await ExecuteRequestToHttpClientApi(queryString, request.CultureName, request.CancellationToken);

            var deserializedResponse = JsonConvert.DeserializeObject<TModel>(responseContent);

            return deserializedResponse;
        }

        private async Task<string> ExecuteRequestToHttpClientApi(string queryString, string cultureName, CancellationToken cancellationToken)
        {
            var httpResponse = await _httpClientExecutorFactory.ExecuteGetAsync(HttpClientConnections.ContentDeliveryApi, queryString,
                new HttpClientExecutorExtras
                {
                    Headers = new Dictionary<string, string> { { "Accept-Language", cultureName } },
                    CancellationToken = cancellationToken
                });

            httpResponse.EnsureSuccessStatusCode();
            var responseContent = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            return responseContent;
        }

        public void Dispose()
        {
            _httpClientExecutorFactory?.Dispose();
        }
    }
}
