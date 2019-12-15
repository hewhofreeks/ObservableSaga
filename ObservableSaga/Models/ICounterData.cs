using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObservableSaga.Models
{
    public interface ICounterData
    {
        string CounterID { get; set; }
        int Count { get; set; }
    }
}
