using System;
using Application.Infrastructure.HttpClients.Interfaces;
using Application.Infrastructure.Polly;
using HttpClientExecutor.Connections;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.ServiceRegistrations
{
    public static class RegisterHttpClientsExtension
    {
        public static void RegisterHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHttpClientExecutor, ContentDeliveryApiHttpClient>();
            
            services.AddHttpClient<ContentDeliveryApiHttpClient>(
                    $"ContentDeliveryApi", c =>
                    {
                        c.BaseAddress = new Uri(configuration["ContentDeliveryApi:BaseAddress"]);
                        c.DefaultRequestHeaders.Add("Accept", "application/json");
                    })
                .AddPolicyHandler(ExternalApiRetryPolicy.GetRetryPolicy());
        }
    }
}
