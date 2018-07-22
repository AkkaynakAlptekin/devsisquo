using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.WebJobs.ServiceBus;

namespace Function
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run([EventHubTrigger("raptorhub", Connection = "EventHubCS")]string myEventHubMessage, TraceWriter log)
        {
            log.Info($"C# Event Hub trigger function processed a message: {myEventHubMessage}");
        }
    }
}
