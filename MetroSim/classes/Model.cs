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
        private Dictionary<string, int> pocetSouprav;
        private string settingsPath = "files/stanice.txt";
        private Kalendar kalendar;
        private int cas;
        public int vysledek;
        private MainGUI gui;
        public bool jeKonec;
        private Nastaveni nastaveni;

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
            kalendar = new Kalendar();

            seznamStanic = new SeznamStanic(StaniceLoader.nactiStanice(settingsPath));

            Console.WriteLine("Načteno " + seznamStanic.stanice.Count + " stanic");
            Console.WriteLine("Počet linek: " + seznamStanic.pismenaLinek.Count);

            setKonecneStanice();

            najdiSousedy();

            seznamPasazeru = new SortedList<int, Pasazer>();

        }

        public void nactiNastaveni(Nastaveni n)
        {
            nastaveni = n;
            cas = 0;
            jeKonec = false;
            vysledek = -1;

            seznamSouprav = new SortedList<string, Souprava>();
            pocetSouprav = new Dictionary<string, int>(); //dictionary ktery pocita pocet souprav na linkach

            foreach(KeyValuePair<string, string> k in seznamStanic.pismenaLinek)
            {
                pocetSouprav.Add(k.Key, 0);
            }

            if (nastaveni.pocatecniStanice != null && nastaveni.konecnaStanice != null)
            {
                pridejHlavnihoPasazera(nastaveni.pocatecniStanice, nastaveni.konecnaStanice, nastaveni.casPrichodu);
            }
            else
            {
                Console.WriteLine("chyba");
            }

            spawniPocatecniSoupravy();
        }

        private void pridejSoupravu(Souprava s)
        {
            seznamSouprav.Add(s.id, s);
            pocetSouprav[s.pismeno]++;
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

        private void spawniSoupravu(Souprava s, int cas) //prida soupravu do kalendar
        {
            kalendar.pridejUdalost(new Udalost(cas, s, TypUdalosti.prijezdDoStanice));
        }


        public void spawniPocatecniSoupravy()
        {
            foreach (KeyValuePair<string, Stanice[]> k in konecneStanice)
            {
                Souprava s = new Souprava(this, k.Key + pocetSouprav[k.Key], k.Key, true, 
                    nastaveni.nastaveniLinek[k.Key].rychlostSouprav,
                    nastaveni.nastaveniLinek[k.Key].kapacitaSouprav,
                    nastaveni.nastaveniLinek[k.Key].dobaCekaniVeStanici, 
                    k.Value[0]); //souprava na pocatecni stanici
                pridejSoupravu(s);
                pocetSouprav[k.Key]++;
                spawniSoupravu(s, 0);

                s = new Souprava(this, k.Key + pocetSouprav[k.Key], k.Key, false,
                    nastaveni.nastaveniLinek[k.Key].rychlostSouprav,
                    nastaveni.nastaveniLinek[k.Key].kapacitaSouprav,
                    nastaveni.nastaveniLinek[k.Key].dobaCekaniVeStanici,
                    k.Value[1]); //souprava na konci v protismeru
                pridejSoupravu(s);
                pocetSouprav[k.Key]++;
                spawniSoupravu(s, 0);
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
