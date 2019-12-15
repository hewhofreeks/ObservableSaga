using Microsoft.AspNetCore.SignalR;
using NServiceBus;
using ObservableSaga.Messages.Commands;
using ObservableSaga.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObservableSaga.Hubs
{
    public class CounterHub : Hub<ICounterHub>
    {
        private readonly IMessageSession _messageSession;

        public CounterHub(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        public async Task SubscribeToCounter(string counterID)
        {
            await _messageSession.SendLocal(new SubscribeToCounter { CounterID = counterID, ConnectionID = this.Context.ConnectionId });

        }
    }

    public interface ICounterHub
    {
        Task Update(ICounterData counterData);
    }
}
