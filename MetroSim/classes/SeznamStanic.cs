using System;
using System.Collections.Generic;

namespace MetroSim
{
    class SeznamStanic
    {
        public SortedList<string, Stanice> stanice;
        public SortedList<string, string> pismenaLinek;

        public SeznamStanic(SortedList<string, Stanice> seznam)
        {
            this.stanice = seznam;
            pismenaLinek = new SortedList<string, string>();
            foreach(KeyValuePair<string, Stanice> k in stanice)
            {
                string pismeno = k.Value.pismeno;
                if (!pismenaLinek.ContainsKey(pismeno))
                {
                    pismenaLinek.Add(pismeno, pismeno);
                }
            }
        }
    }
}
