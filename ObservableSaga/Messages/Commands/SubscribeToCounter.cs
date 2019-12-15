using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObservableSaga.Messages.Commands
{
    public class SubscribeToCounter : IMessage
    {
        public string CounterID { get; set; }

        public string ConnectionID { get; set; }
    }
}
