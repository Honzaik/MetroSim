﻿using System;
using System.Collections.Generic;

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
        private Random rand;
        private SpawnerSouprav spawnerSouprav;
        private static int SPAWN_SOUPRAV_MEZICAS = 5; 

        public Model(MainGUI gui, Random rand)
        {
            this.rand = rand;
            this.gui = gui;
        }

        public Model(MainGUI gui, Random rand, string settingsPath)
        {
            this.rand = rand;
            this.gui = gui;
            this.settingsPath = settingsPath;
        }

        public void init()
        {
            seznamStanic = new SeznamStanic(StaniceLoader.nactiStanice(settingsPath));

            Console.WriteLine("Načteno " + seznamStanic.stanice.Count + " stanic");
            Console.WriteLine("Počet linek: " + seznamStanic.pismenaLinek.Count);

            setKonecneStanice();

            najdiSousedy();
            spawnerSouprav = new SpawnerSouprav(this, "spawner");
        }

        public void reset()
        {
            kalendar = new Kalendar();
            seznamPasazeru = new SortedList<int, Pasazer>();

            foreach(KeyValuePair<string, Stanice> k in seznamStanic.stanice)
            {
                k.Value.reset();
            }
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

            naplanujSpawnSouprav();
            spawniOstatniPasazery(); //kolik za jednotku casu se spawne
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
            Udalost prichodPrvnihoPasazera = new Udalost(start, hlavni, TypUdalosti.prichodDoStanice);
            kalendar.pridejUdalost(prichodPrvnihoPasazera);
        }

        private Pasazer vygenerujPasazera()
        {
            int pocatecniStanice = rand.Next(0, seznamStanic.stanice.Count);
            int konecnaStanice = rand.Next(0, seznamStanic.stanice.Count);
            while (konecnaStanice == pocatecniStanice)
            {
                konecnaStanice = rand.Next(0, seznamStanic.stanice.Count); //aby se generovali pasazeri, kteri nikam nejedou v podstate
            }
            Pasazer p = new Pasazer(this, seznamPasazeru.Count + "", seznamStanic.stanice.Values[pocatecniStanice], seznamStanic.stanice.Values[konecnaStanice], cas);
            return p;
        }

        private void spawniOstatniPasazery()
        {
            int posledniSpawnCas = seznamPasazeru[seznamPasazeru.Count-1].start; //cas kdy se naposledy spawnovali novi lide
            int pocetLidiKVygenerovani = (cas - posledniSpawnCas) * nastaveni.frekvenceLidi;
            if(pocetLidiKVygenerovani == 0) //zacatek napřiklad (vždycky nekoho vygeneruj, když se změní čas)
            {
                pocetLidiKVygenerovani = nastaveni.frekvenceLidi;
            }
            for(int i = 0; i < pocetLidiKVygenerovani; i++)
            {
                Pasazer p = vygenerujPasazera();
                seznamPasazeru.Add(seznamPasazeru.Count, p);
                Udalost prichodPasazera = new Udalost(cas, p, TypUdalosti.prichodDoStanice);
                kalendar.pridejUdalost(prichodPasazera);
            }
        }

        private void spawniSoupravu(Souprava s, int cas) //prida soupravu do kalendar
        {
            kalendar.pridejUdalost(new Udalost(cas, s, TypUdalosti.prijezdDoStanice));
        }

        private void pridejSoupravu(Souprava s)
        {
            seznamSouprav.Add(s.id, s);
            pocetSouprav[s.pismeno]++;
        }

        public void spawniCastSouprav()
        {
            foreach (KeyValuePair<string, Stanice[]> k in konecneStanice)
            {
                if (nastaveni.nastaveniLinek[k.Key].pocetSouprav > pocetSouprav[k.Key]) //jeste je potreba spawnout
                {
                    Souprava s = new Souprava(this, k.Key + pocetSouprav[k.Key], k.Key, true,
                    nastaveni.nastaveniLinek[k.Key].rychlostSouprav,
                    nastaveni.nastaveniLinek[k.Key].kapacitaSouprav,
                    nastaveni.nastaveniLinek[k.Key].dobaCekaniVeStanici,
                    k.Value[0]); //souprava na pocatecni stanici
                    pridejSoupravu(s);
                    spawniSoupravu(s, cas);

                    s = new Souprava(this, k.Key + pocetSouprav[k.Key], k.Key, false,
                        nastaveni.nastaveniLinek[k.Key].rychlostSouprav,
                        nastaveni.nastaveniLinek[k.Key].kapacitaSouprav,
                        nastaveni.nastaveniLinek[k.Key].dobaCekaniVeStanici,
                        k.Value[1]); //souprava na konci v protismeru
                    pridejSoupravu(s);
                    spawniSoupravu(s, cas);
                }
                else
                {
                    Console.WriteLine("přeskakuju " + k.Key + " pocet souprav je " + pocetSouprav[k.Key] + " kapacita: " + nastaveni.nastaveniLinek[k.Key].pocetSouprav);
                }
            }
        }

        public void naplanujSpawnSouprav() //naplanuje udalost pro spawn souprav v urcite casy
        {
            int maxPocetSouprav = 2;
            foreach(KeyValuePair<string, string> k in seznamStanic.pismenaLinek)
            {
                if(nastaveni.nastaveniLinek[k.Value].pocetSouprav > maxPocetSouprav)
                {
                    maxPocetSouprav = nastaveni.nastaveniLinek[k.Value].pocetSouprav;
                }
            }

            for(int i = 0; i < maxPocetSouprav/2; i++)
            {
                kalendar.pridejUdalost(new Udalost(SPAWN_SOUPRAV_MEZICAS * i, spawnerSouprav, TypUdalosti.spawnSouprav));
                Console.WriteLine("BUDU SPAWNOVAT SOUPRAVY V " + SPAWN_SOUPRAV_MEZICAS * i);
            }
        }

        public void spocitej()
        {
            bool spawnNew = false;
            while (!kalendar.jePrazdny() && !jeKonec)
            {
                Udalost zpracovavanaUdalost = kalendar.vratNejaktualnejsi();
                if(zpracovavanaUdalost.kdy > cas) //pokud se nekam posunul cas -> spawni nove lidi
                {
                    spawnNew = true;
                }
                else
                {
                    spawnNew = false;
                }
                cas = zpracovavanaUdalost.kdy;
                if (spawnNew)
                {
                    spawniOstatniPasazery();
                }

                (zpracovavanaUdalost.kdo).zpracuj(zpracovavanaUdalost);
                
                if (cas > 50000)
                {
                    Console.WriteLine("STOPPED BECAUSE TIME LIMIT EXCEEDED " + cas);
                    cas = -1; //chyba
                    break;
                }
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
