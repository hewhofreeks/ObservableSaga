using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObservableSaga.Messages.Commands
{
    public class SubtractFromCounter
    {
        public string CounterID { get; set; }
    }
}
