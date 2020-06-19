using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Domain.Http.Clients.ContentDeliveryApi
{
    public class ContentDeliveryApiSingleRequest
    {
        public string ContentId { get; set; }
        public string CultureName { get; set; }
        public CancellationToken CancellationToken { get; set; }
    }
}
