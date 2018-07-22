using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.EventHubs.Processor;

namespace Workerino
{
    class TeleEventProcessorFactory : IEventProcessorFactory
    {

        private readonly string _cs;

        public TeleEventProcessorFactory(string cs)
        {
            _cs = cs;
        }


        public IEventProcessor CreateEventProcessor(PartitionContext context)
        {
            return new TeleEventProcessor(context, _cs);
        }
    }
}
