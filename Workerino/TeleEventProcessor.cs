using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Lib;
using Lib.Repos;
using Lib.Repos.Interfaces;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.EventHubs.Processor;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Workerino
{
    public class TeleEventProcessor : IEventProcessor
    {
        private readonly IGeoTrackingRepo _geoTrackingRepo;

        public TeleEventProcessor(PartitionContext context, string cs)
        {
            _geoTrackingRepo = new GeoTrackingRepo(cs);

        }


        public Task CloseAsync(PartitionContext context, CloseReason reason)
        {
            Console.WriteLine($"Processor Shutting Down. Partition '{context.PartitionId}', Reason: '{reason}'.");
            return Task.CompletedTask;
        }

        public Task OpenAsync(PartitionContext context)
        {
            Console.WriteLine($"SimpleEventProcessor initialized. Partition: '{context.PartitionId}'");
            return Task.CompletedTask;
        }

        public Task ProcessErrorAsync(PartitionContext context, Exception error)
        {
            Console.WriteLine($"Error on Partition: {context.PartitionId}, Error: {error.Message}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Processes the message on the eventHub
        /// </summary>
        /// <param name="context"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        public async Task ProcessEventsAsync(PartitionContext context, IEnumerable<EventData> messages)
        {

            foreach (var eventData in messages)
            {

                var json = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);

                Console.WriteLine($"{json}");
                var data = JsonConvert.DeserializeObject<JObject>(json);
                var type = data.Value<string>("Type");

                switch (type)
                {
                    case "MawTelemetry":
                        var msg = data.Value<string>("Msg");
                        var content = JsonConvert.DeserializeObject<JObject>(msg);
                        var trackingJson = content.Value<string>("Tracking");
                        var tracking = JsonConvert.DeserializeObject<GeoTracking>(trackingJson);
                        var raptorDataJson = content.Value<string>("Data");
                        var raptorData = JsonConvert.DeserializeObject<RaptorMetaData>(raptorDataJson);
                        await _geoTrackingRepo.InsertWithMetaData(tracking, raptorData);
                        break;
                    case "Telemetry":
                        var msg2 = data.Value<string>("Msg");
                        var geoTracking2 = JsonConvert.DeserializeObject<GeoTracking>(msg2);
                        await _geoTrackingRepo.Insert(geoTracking2);
                        break;
                    case "AssignGeneticMutation":
                        //TODO: implement
                        break;
                }


            }

            await context.CheckpointAsync();
        }
    }
}
