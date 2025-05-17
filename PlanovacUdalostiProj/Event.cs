using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanovacUdalostiProj
{
    public class Event
    {
        public string Name;
        public DateTime Date;

        public Event(string name, DateTime date)
        {
            Name = name;
            Date = date;
        }
    }
}