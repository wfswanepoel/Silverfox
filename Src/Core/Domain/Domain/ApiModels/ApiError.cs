using System;
using System.Collections.Generic;
using System.Text;
using Domain.Errors;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.ApiModels
{
    [JsonObject]
    [Serializable]
    public class ApiError
    {
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public ErrorTypes Type { get; set; }
        
        [JsonProperty]
        public string Detail { get; set; }

        public int? Code { get; set; }

        public int? StatusCode { get; set; }
    }
}
