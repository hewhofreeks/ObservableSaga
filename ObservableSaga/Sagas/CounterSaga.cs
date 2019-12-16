using Microsoft.AspNetCore.SignalR;
using NServiceBus;
using ObservableSaga.Hubs;
using ObservableSaga.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObservableSaga.Sagas
{

    
    public class CounterSaga : Saga<CounterSagaData>,
        IAmStartedByMessages<SubscribeToCounter>,
        IHandleMessages<AddToCounter>,
        IHandleMessages<SubtractFromCounter>
    {
        private IHubContext<CounterHub, ICounterHub> _counterHub;

        public CounterSaga(IHubContext<CounterHub,ICounterHub> counterHub)
        {
            _counterHub = counterHub;
        }

        public async Task Handle(SubscribeToCounter message, IMessageHandlerContext context)
        {
            await _counterHub.Groups.AddToGroupAsync(message.ConnectionID, message.CounterID);
        }

        public async Task Handle(AddToCounter message, IMessageHandlerContext context)
        {
            this.Data.Count++;
        }


        public async Task Handle(SubtractFromCounter message, IMessageHandlerContext context)
        {
            this.Data.Count--;
        }

        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<CounterSagaData> mapper)
        {
            mapper.ConfigureMapping<SubscribeToCounter>(m => m.CounterID).ToSaga(m => m.CounterID);

            mapper.ConfigureMapping<AddToCounter>(m => m.CounterID).ToSaga(m => m.CounterID);

            mapper.ConfigureMapping<SubtractFromCounter>(m => m.CounterID).ToSaga(m => m.CounterID);
        }
    }
}
