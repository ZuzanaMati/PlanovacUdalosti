using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanovacUdalostiProj
{
    public class EventManager
    {
        public static bool CheckIfEventListIsEmpty(List<Event> events)
        {
            if (events.Count == 0)
            {
                Console.WriteLine("Zatím nemáš žádné naplánované události.");
                return true;
            }

            return false;
        }

        public static void PrintEventList(List<Event> events)
        {
            foreach (Event e in events.OrderBy(e => e.Date))
            {
                int daysDiff = (e.Date - DateTime.Today).Days;

                if (daysDiff >= 0)
                {
                    Console.WriteLine($"Event {e.Name} s datem {e.Date:yyyy-MM-dd} se uskuteční za {daysDiff} dní");
                }
                else
                {
                    Console.WriteLine($"Event {e.Name} s datem {e.Date:yyyy-MM-dd} proběhl před {-daysDiff} dny");
                }
            }
        }

        public static void PrintEventStats(List<Event> events)
        {
            var stats = new Dictionary<DateTime, int>();

            foreach (Event e in events)
            {
                DateTime dateOnly = e.Date.Date;
                if (stats.ContainsKey(dateOnly))
                {
                    stats[dateOnly]++;
                }
                else
                {
                    stats[dateOnly] = 1;
                }
            }

            foreach (var stat in stats.OrderBy(e => e.Key))
            {
                Console.WriteLine($"Datum: {stat.Key:yyyy-MM-dd}: eventy: {stat.Value}");
            }
        }

    }
}