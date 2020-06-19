using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Application.Infrastructure.HttpClients.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HttpClientExecutor.Connections
{
    public class ContentDeliveryApiHttpClient : HttpClientExecutor
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ContentDeliveryApiHttpClient(IConfiguration configuration, IHttpClientFactory httpClientFactory) : base(configuration)
        {
            _httpClientFactory = httpClientFactory;
        }

        protected override void AddAuthorizationTokenToClientHeader(IAuthenticateResponse authenticateResponse)
        {
            ExecuterHttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authenticateResponse.AccessToken);
            ExecuterHttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected override async Task<IAuthenticateResponse> Authenticate()
        {
            var authFields = HttpClientExecutorOptions.ToBearerAuthenticateDictionary();

            var authHttpResponse = await ExecuterHttpClient.PostAsync(HttpClientExecutorOptions.AuthenticateRequestUri,
                new FormUrlEncodedContent(authFields));

            authHttpResponse.EnsureSuccessStatusCode();

            var content = authHttpResponse.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IAuthenticateResponse>(content);
        }

        protected override HttpClient CreateExecuterHttpClient()
        {
            ExecuterHttpClient = _httpClientFactory.CreateClient("ContentDeliveryApi");
            return ExecuterHttpClient;
        }
    }
}
