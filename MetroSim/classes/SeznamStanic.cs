using System;
using System.Collections.Generic;

namespace MetroSim
{
    class SeznamStanic
    {
        public SortedList<string, Stanice> stanice;
        public SortedList<string, Stanice[]> konecneStanice;
        public SortedList<string, string> pismenaLinek;

        public SeznamStanic(SortedList<string, Stanice> seznam)
        {
            this.stanice = seznam;
            setPismenaLinek();
            setKonecneStanice();
            najdiSousedy();
        }

        private void setPismenaLinek()
        {
            pismenaLinek = new SortedList<string, string>();
            foreach (KeyValuePair<string, Stanice> k in stanice)
            {
                string pismeno = k.Value.pismeno;
                if (!pismenaLinek.ContainsKey(pismeno))
                {
                    pismenaLinek.Add(pismeno, pismeno);
                }
            }
        }

        private void setKonecneStanice()
        {
            konecneStanice = new SortedList<string, Stanice[]>();
            foreach (KeyValuePair<string, Stanice> k in stanice)
            {
                if (k.Value.jeKonecna)
                {
                    if (konecneStanice.ContainsKey(k.Value.pismeno)) //uz je najita jedna konecna stanice
                    {
                        Stanice[] stanice = konecneStanice[k.Value.pismeno];
                        if (stanice[0] == null)
                        {
                            stanice[0] = k.Value;
                        }
                        else
                        {
                            stanice[1] = k.Value;
                        }
                    }
                    else
                    {
                        Stanice[] stanice = new Stanice[2];
                        if (k.Value.kilometr == 0)
                        {
                            stanice[0] = k.Value;
                        }
                        else
                        {
                            stanice[1] = k.Value;
                        }
                        konecneStanice.Add(k.Value.pismeno, stanice);
                    }
                }
            }
        }

        private void najdiSousedy()
        {
            foreach (KeyValuePair<string, Stanice> k in stanice)
            {
                k.Value.najdiSousedy(stanice);
            }
        }
    }
}
