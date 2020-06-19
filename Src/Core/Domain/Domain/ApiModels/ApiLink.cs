using System;
using System.Collections.Generic;
using System.Text;
using Domain.Http.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.ApiModels
{
    public class ApiLink
    {
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Href { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public HttpMethods Method { get; set; }
        public string Title { get; set; }
        public string ContentId { get; set; }
    }
}
