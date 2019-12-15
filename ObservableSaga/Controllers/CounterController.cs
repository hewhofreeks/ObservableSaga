using Microsoft.AspNetCore.Mvc;
using NServiceBus;
using ObservableSaga.Messages.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObservableSaga.Controllers
{
    public class CounterController: Controller
    {
        private readonly IMessageSession _messageSession;

        public CounterController(IMessageSession messageSession)
        {
            _messageSession = messageSession;
        }

        [HttpPost]
        public async Task Add([FromQuery] string counterID)
        {
            await _messageSession.SendLocal(new AddToCounter { CounterID = counterID });
        }

        [HttpPost]
        public async Task Subtract([FromQuery] string counterID)
        {
            await _messageSession.SendLocal(new SubtractFromCounter { CounterID = counterID });
        }
    }
}
