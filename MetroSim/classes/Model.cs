using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MetroSim
{
    class Model
    {

        private SeznamStanic seznamStanic;
        private SortedList<int, Pasazer> seznamPasazeru;
        private SortedList<string, Souprava> seznamSouprav;
        private SortedList<string, Stanice[]> konecneStanice;
        private int[] pocetSouprav;
        private string settingsPath = "files/stanice.txt";
        private Kalendar kalendar;
        private int cas;
        public int vysledek;
        private MainGUI gui;
        public bool jeKonec;

        public Model(MainGUI gui)
        {
            this.gui = gui;
        }

        public Model(MainGUI gui, string settingsPath)
        {
            this.gui = gui;
            this.settingsPath = settingsPath;
        }

        public void init()
        {
            jeKonec = false;
            cas = 0;
            vysledek = -1;
            kalendar = new Kalendar();

            seznamStanic = new SeznamStanic(StaniceLoader.nactiStanice(settingsPath));

            Console.WriteLine("Načteno " + seznamStanic.stanice.Count + " stanic");
            Console.WriteLine("Počet linek: " + seznamStanic.pismenaLinek.Count);

            setKonecneStanice();

            vytvorPocatecniSoupravy(0.3f, 40);

            najdiSousedy();

            seznamPasazeru = new SortedList<int, Pasazer>();

        }

        public void nactiNastaveni(Nastaveni nastaveni)
        {

        }

        private void vytvorPocatecniSoupravy(float rychlost, int kapacita)
        {
            seznamSouprav = new SortedList<string, Souprava>();
            pocetSouprav = new int[konecneStanice.Count];
            Souprava s = null;
            int index = 0;
            foreach (KeyValuePair<string, Stanice[]> k in konecneStanice)
            {
                s = new Souprava(this, k.Key + pocetSouprav[index], k.Key, true, rychlost, kapacita, 5, k.Value[0]); //souprava na pocatecni stanici
                seznamSouprav.Add(k.Key + pocetSouprav[index], s);
                pocetSouprav[index]++;
                s = new Souprava(this, k.Key + pocetSouprav[index], k.Key, false, rychlost, kapacita, 5, k.Value[1]); //souprava na konci v protismeru
                seznamSouprav.Add(k.Key + pocetSouprav[index], s);
                pocetSouprav[index]++;
                index++;
            }
        }

        private void setKonecneStanice()
        {
            konecneStanice = new SortedList<string, Stanice[]>();
            foreach (KeyValuePair<string, Stanice> k in seznamStanic.stanice)
            {
                if (k.Value.jeKonecna)
                {
                    if (konecneStanice.ContainsKey(k.Value.pismeno)) //uz je najita jedna konecna stanice
                    {
                        Stanice[] stanice = konecneStanice[k.Value.pismeno];
                        if(stanice[0] == null)
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
                        if(k.Value.kilometr == 0)
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
            foreach(KeyValuePair<string, Stanice> k in seznamStanic.stanice)
            {
                k.Value.najdiSousedy(seznamStanic.stanice);
            }
        }

        public SeznamStanic getSeznamStanic()
        {
            return seznamStanic;
        }

        public void pridejHlavnihoPasazera(Stanice zacatek, Stanice konec, int start)
        {
            Pasazer hlavni = new Pasazer(this, "0", zacatek, konec, start);
            seznamPasazeru.Add(seznamPasazeru.Count, hlavni);
            Udalost prichodPrvnihoPasazera = new Udalost(cas, hlavni, TypUdalosti.prichodDoStanice);
            kalendar.pridejUdalost(prichodPrvnihoPasazera);
        }

        public void spawniSoupravy()
        {
            foreach(KeyValuePair<string, Souprava> k in seznamSouprav)
            {
                kalendar.pridejUdalost(new Udalost(cas, k.Value, TypUdalosti.prijezdDoStanice));
            }
        }

        public void spocitej()
        {
            while (!kalendar.jePrazdny() && !jeKonec)
            {
                Udalost zpracovavanaUdalost = kalendar.vratNejaktualnejsi();
                cas = zpracovavanaUdalost.kdy;
                (zpracovavanaUdalost.kdo).zpracuj(zpracovavanaUdalost);
                if (cas > 300) break;
            }
            vysledek = cas;
            gui.finished(vysledek);
        }

        public void pridejDoKalendare(Udalost u)
        {
            kalendar.pridejUdalost(u);
        }

        public void odeberZKalendare(int kdy, Proces kdo, TypUdalosti co)
        {
            kalendar.odeberUdalost(kdy, kdo, co);
        }

        public int getCas()
        {
            return cas;
        }
    }
}
