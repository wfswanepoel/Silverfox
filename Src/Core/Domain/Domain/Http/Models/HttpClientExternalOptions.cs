using System;
using System.Collections.Generic;
using System.Text;
using Domain.Http.Enums;
using Newtonsoft.Json;

namespace Domain.Http.Models
{
    [JsonObject]
    public class HttpClientExternalOptions
    {
        [JsonProperty]
        public string BaseAddress { get; set; }
        [JsonProperty]
        public int Timeout { get; set; }
        [JsonProperty("grant_type")]
        public string GrandType { get; set; }
        [JsonProperty("username")]
        public string UserName { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        public string AuthenticateRequestUri { get; set; }
        public bool UseAuthorizationRequest { get; set; }
        public HttpClientConnections HttpClientConnection { get; set; }
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        public Dictionary<string, string> ToBearerAuthenticateDictionary()
        {
            return new Dictionary<string, string>
            {
                { "grant_type", GrandType },
                { "username", UserName },
                { "password", Password }
            };
        }
    }
}
