using Microsoft.AspNetCore.SignalR;
using NServiceBus.Pipeline;
using NServiceBus.Sagas;
using ObservableSaga.Hubs;
using ObservableSaga.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObservableSaga.NServiceBusConfig.Features
{
    public class SignalRUpdateBehavior : Behavior<IInvokeHandlerContext>
    {
        private readonly IHubContext<CounterHub, ICounterHub> _counterHub;

        public SignalRUpdateBehavior(IHubContext<CounterHub, ICounterHub> counterHub)
        {
            _counterHub = counterHub;
        }

        public override async Task Invoke(IInvokeHandlerContext context, Func<Task> next)
        {
            await next()
                .ConfigureAwait(false);
            if (context.Extensions.TryGet(out ActiveSagaInstance activeSagaInstance))
            {
                var instance = activeSagaInstance.Instance.Entity;

                if(instance is ICounterData)
                {
                    var counterData = instance as ICounterData;
                    await _counterHub.Clients.Group(counterData.CounterID).Update(counterData);
                }
            }
        }
    }
}
