using System.Globalization;

namespace PlanovacUdalostiProj;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Zadej:");
        Console.WriteLine(" - EVENT;Nazev;YYYY-MM-DD");
        Console.WriteLine(" - LIST pro vypis");
        Console.WriteLine(" - STATS pro statistiky");
        Console.WriteLine(" - END pro ukonceni");
        List<Event> events = new List<Event>();
        Dictionary<DateTime, int> stats = new Dictionary<DateTime, int>();

        while (true)
        {

            System.Console.WriteLine("Zadej event: ");
            string entry = Console.ReadLine();

            //ukonci program, kdyz uzivatel napise end
            if (entry.Trim().ToUpper() == "END") break;

            try
            {

                // Uzivatel chce zadat event
                if (entry.Trim().ToUpper().StartsWith("EVENT"))
                {

                    string[] entrySplitted = entry.Split(";");
                    // Kontrola formátu
                    if (entrySplitted.Length != 3)
                    {
                        Console.WriteLine("Špatný formát. Zadej: EVENT;Název;YYYY-MM-DD");
                        continue;
                    }
                    //Parse the user input
                    string eventName = entrySplitted[1];
                    string entryDate = entrySplitted[2];

                    //Parse the date and create datetime entry
                    if (DateTime.TryParseExact(entryDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime eventDate))
                    {
                        //Vytvoreni eventy
                        Event newEvent = new Event(eventName, eventDate);
                        events.Add(newEvent);
                    }
                    else
                    {
                        Console.WriteLine("Chyba: Datum musi byt ve formatu YYYY-MM-DD (napr. 2025-05-17).");
                    }


                }
                else if (entry.Trim().ToUpper() == "LIST")
                {
                    //Jeslize jeste nejsou zadne udalost
                    if (events.Count == 0)
                    {
                        Console.WriteLine("Zatím nemáš žádné naplánované události.");
                        continue;
                    }

                    //spocitej rozdil mesi datem eventu a dnesnim datem
                    foreach (Event e in events.OrderBy(e => e.Date))
                    {
                        int daysDiff = (e.Date - DateTime.Today).Days;

                        //vypis pouze udalosti, ktere maji teprve nasledovat
                        if (daysDiff >= 0)
                        {
                            System.Console.WriteLine($"Event {e.Name} s datem {e.Date:yyyy-MM-dd} se uskutecni za {daysDiff} dni");
                        }
                        else
                        {
                            Console.WriteLine($"Event {e.Name} s datem {e.Date:yyyy-MM-dd} probehl pred {-daysDiff} dny");
                        }
                    }
                }
                else if (entry.Trim().ToUpper() == "STATS")
                {

                    //Jestli jeste nejsou zadne udalosti
                    if (events.Count == 0)
                    {
                        Console.WriteLine("Žádné události k vyhodnocení statistik.");
                        continue;
                    }

                    stats = new Dictionary<DateTime, int>();

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

                    foreach (var e in stats.OrderBy(e => e.Key))
                    {
                        Console.WriteLine("Datum: " + e.Key.ToString("yyyy-MM-dd") + ": eventy: " + e.Value);
                    }

                }
                else
                {
                    Console.WriteLine("Spatny format. Pouzij: EVENT;Nazev;YYYY-MM-DD");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Neocekavana chyba. Zkus to prosim znovu.");
            }
        }


    }
}
