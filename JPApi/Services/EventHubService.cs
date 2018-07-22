using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPApi.Services.Interfaces;
using Microsoft.Azure.EventHubs;

namespace JPApi.Services
{
    public class EventHubService : IEventHubService
    {
        private readonly EventHubClient _ehc;

        public EventHubService(string cs)
        {
            _ehc = EventHubClient.CreateFromConnectionString(cs);
        }


        public async Task SendMsg(string msg)
        {
            await _ehc.SendAsync(new EventData(Encoding.UTF8.GetBytes(msg)));
        }
    }
}
