using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Domain.ApiModels
{
    [JsonObject]
    [Serializable]
    public sealed class ApiResponse<TData>
        where TData : class
    {
        public TData Data { get; set; }
        public ApiError Error { get; set; } = new ApiError();
        public List<ApiLink> Links { get; } = new List<ApiLink>();
    }
}
