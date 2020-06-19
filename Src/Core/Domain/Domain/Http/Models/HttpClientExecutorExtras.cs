using System.Collections.Generic;
using System.Threading;

namespace Domain.Http.Models
{
    public class HttpClientExecutorExtras
    {
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public CancellationToken CancellationToken { get; set; }
    }
}
