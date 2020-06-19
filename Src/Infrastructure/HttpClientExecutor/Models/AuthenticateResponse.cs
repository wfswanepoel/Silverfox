using Application.Infrastructure.HttpClients.Interfaces;
using Newtonsoft.Json;

namespace HttpClientExecutor.Models
{
    [JsonObject]
    public class AuthenticateResponse : IAuthenticateResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
}
