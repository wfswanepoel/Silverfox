using System;
using System.Linq;
using System.Net.Http;
using Domain.Constants;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Application.Infrastructure.Polly
{
    public static class ExternalApiRetryPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(request => HttpConstants.HttpStatusCodesWorthRetrying.Contains(request.StatusCode))
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(30)
                });
        }
    }
}
