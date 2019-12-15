using NServiceBus;
using ObservableSaga.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ObservableSaga.Sagas
{
    public class CounterSagaData : ContainSagaData, ICounterData
    {
        public string CounterID { get; set; }
        public int Count { get; set; }
    }


}
