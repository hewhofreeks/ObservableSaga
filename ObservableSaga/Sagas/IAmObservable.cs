using Microsoft.AspNetCore.SignalR;
using NServiceBus;
using ObservableSaga.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObservableSaga.Sagas
{
    public interface IAmObservable
    {
        Task UpdateClients();
    }
}
