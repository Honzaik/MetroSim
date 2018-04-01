using System;
using System.Collections.Generic;

namespace MetroSim
{
    class Model
    {
        private SeznamStanic seznamStanic;
        private SortedList<int, Pasazer> seznamPasazeru;
        private SortedList<string, Souprava> seznamSouprav;
        private Dictionary<string, int> pocetSouprav;
        private string settingsPath = "stanice.txt";
        private Kalendar kalendar;
        private int cas;
        public int vysledek;
        private MainGUI gui;
        public bool jeKonec;
        private Nastaveni nastaveni;
        private Random rand;
        private SpawnerSouprav spawnerSouprav;
        private static int SPAWN_SOUPRAV_MEZICAS = 5;
        private static int SPAWN_LIDI_MEZICAS = 30;
        private string id;
        private int pocetVygenerovanychPasazeru = 0;

        public Model(MainGUI gui, string id)
        {
            this.rand = new Random(this.GetHashCode());
            this.gui = gui;
            this.id = id;
            spawnerSouprav = new SpawnerSouprav(this, "spawner");
            seznamStanic = new SeznamStanic(StaniceLoader.nactiStanice(settingsPath));
        }

        public Model(MainGUI gui, string id, string settingsPath)
        {
            this.rand = new Random(this.GetHashCode());
            this.gui = gui;
            this.id = id;
            this.settingsPath = settingsPath;
            spawnerSouprav = new SpawnerSouprav(this, "spawner");
            seznamStanic = new SeznamStanic(StaniceLoader.nactiStanice(settingsPath));
        }

        public void reset()
        {
            cas = 0;
            jeKonec = false;
            vysledek = -1;

            kalendar = new Kalendar();
            seznamPasazeru = new SortedList<int, Pasazer>();
            seznamSouprav = new SortedList<string, Souprava>();
            pocetSouprav = new Dictionary<string, int>(); //dictionary ktery pocita pocet souprav na linkach
            foreach (KeyValuePair<string, string> k in seznamStanic.pismenaLinek) //inicializuje Dictionary
            {
                pocetSouprav.Add(k.Key, 0);
            }

            foreach (KeyValuePair<string, Stanice> k in seznamStanic.stanice)
            {
                k.Value.reset();
            }
        }

        public void nactiNastaveni(Nastaveni n)
        {
            nastaveni = n;

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

        public SeznamStanic getSeznamStanic()
        {
            return seznamStanic;
        }

        public void pridejHlavnihoPasazera(Stanice zacatek, Stanice konec, int start)
        {
            Pasazer hlavni = new Pasazer(this, "0", zacatek, konec, start);
            seznamPasazeru.Add(pocetVygenerovanychPasazeru, hlavni);
            pocetVygenerovanychPasazeru++;
            Udalost prichodPrvnihoPasazera = new Udalost(start, hlavni, TypUdalosti.prichodDoStanice);
            kalendar.pridejUdalost(prichodPrvnihoPasazera);
        }

        private Pasazer vygenerujPasazera(int casPrichodu)
        {
            int pocatecniStanice = rand.Next(0, seznamStanic.stanice.Count);
            int konecnaStanice = rand.Next(0, seznamStanic.stanice.Count);
            while (konecnaStanice == pocatecniStanice)
            {
                konecnaStanice = rand.Next(0, seznamStanic.stanice.Count); //aby se generovali pasazeri, kteri nikam nejedou v podstate
            }
            Pasazer p = new Pasazer(this, pocetVygenerovanychPasazeru + "", seznamStanic.stanice.Values[pocatecniStanice], seznamStanic.stanice.Values[konecnaStanice], casPrichodu);
            return p;
        }

        private void spawniOstatniPasazery() //kazdejch 60 spawni lidi
        {
            int pocetLidiKVygenerovani = SPAWN_LIDI_MEZICAS * nastaveni.frekvenceLidi;
            for (int i = 0; i < pocetLidiKVygenerovani; i++)
            {
                int casPrichodu = rand.Next(cas, cas + SPAWN_LIDI_MEZICAS);
                Pasazer p = vygenerujPasazera(casPrichodu);
                pocetVygenerovanychPasazeru++;
                seznamPasazeru.Add(pocetVygenerovanychPasazeru, p);
                Udalost prichodPasazera = new Udalost(p.start, p, TypUdalosti.prichodDoStanice);
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
            foreach (KeyValuePair<string, Stanice[]> k in seznamStanic.konecneStanice)
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
                //Console.WriteLine("BUDU SPAWNOVAT SOUPRAVY V " + SPAWN_SOUPRAV_MEZICAS * i);
            }
        }

        public void spocitej()
        {
            Console.WriteLine("ZAHAJUJU VYPOCET " + this.id);
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
                if (spawnNew && cas % SPAWN_LIDI_MEZICAS == 0)
                {
                    spawniOstatniPasazery();
                }

                (zpracovavanaUdalost.kdo).zpracuj(zpracovavanaUdalost);
                
                if (cas > 5000)
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

        public int getCas()
        {
            return cas;
        }

        public void removePasazer(Pasazer p)
        {
            seznamPasazeru.Remove(Int32.Parse(p.id));
            //Console.WriteLine("removing " + p.id);
        }
    }
}
