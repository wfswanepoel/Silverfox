using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domain.Entities.Images
{
    [Serializable]
    public class ImageResponse
    {
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Href { get; set; }
    }
}
