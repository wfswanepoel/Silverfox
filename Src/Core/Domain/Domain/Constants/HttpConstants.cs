using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Domain.Constants
{
    public struct HttpConstants
    {
        public static readonly HttpStatusCode[] HttpStatusCodesWorthRetrying = {
            HttpStatusCode.RequestTimeout, // 408
            HttpStatusCode.BadGateway, // 502
            HttpStatusCode.ServiceUnavailable, // 503
            HttpStatusCode.GatewayTimeout // 504
        };
    }
}
