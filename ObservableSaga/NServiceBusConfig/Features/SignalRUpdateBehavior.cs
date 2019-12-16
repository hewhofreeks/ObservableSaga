using Microsoft.AspNetCore.SignalR;
using NServiceBus.Pipeline;
using NServiceBus.Sagas;
using ObservableSaga.Hubs;
using ObservableSaga.Models;
using ObservableSaga.Sagas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObservableSaga.NServiceBusConfig.Features
{
    public class SignalRUpdateBehavior : Behavior<IInvokeHandlerContext>
    {

        public SignalRUpdateBehavior()
        {
        }

        public override async Task Invoke(IInvokeHandlerContext context, Func<Task> next)
        {
            await next()
                .ConfigureAwait(false);

            if (context.Extensions.TryGet(out ActiveSagaInstance activeSagaInstance))
            {
                var saga = activeSagaInstance.Instance;

                if(saga.GetType().GetInterfaces().Contains(typeof(IAmObservable)))
                {
                    var observableSaga = saga as IAmObservable;

                    await observableSaga.UpdateClients();
                }
            }
        }
    }
}
