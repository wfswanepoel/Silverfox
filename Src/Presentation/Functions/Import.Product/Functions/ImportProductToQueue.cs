using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Import.Product.Functions
{
    public static class ImportProductToQueue
    {
        [FunctionName("ImportProductToQueue")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
