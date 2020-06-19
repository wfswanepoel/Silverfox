using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Http.Clients.ContentDeliveryApi
{
    public class ContentDeliveryApiSingleResponse
    {
        public string ContentId { get; set; }
        public string DisplayName { get; set; }
        public string SubTitle { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
