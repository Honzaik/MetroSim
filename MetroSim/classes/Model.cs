using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MetroSim
{
    class Model
    {

        private SortedList<string, Stanice> seznamStanic;
        private SortedList<int, Pasazer> seznamPasazeru;
        private SortedList<string, Souprava> seznamSouprav;
        private string settingsPath = "files/stanice.txt";
        private Kalendar kalendar;
        private int cas;
        public int vysledek;
        private MainGUI gui;

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
            cas = 0;
            vysledek = -1;
            kalendar = new Kalendar();

            seznamStanic = SettingsLoader.nactiNastaveni(settingsPath, this);
            Console.WriteLine("Načteno " + seznamStanic.Count + " stanic");

            najdiSousedy();

            Stanice stanice1 = seznamStanic.Values[9];
            Console.WriteLine(stanice1.id + " " + stanice1.jmeno + " pocet: " + stanice1.pocetSousedu());
            stanice1.vypisSousedy();

            seznamPasazeru = new SortedList<int, Pasazer>();
            seznamSouprav = new SortedList<string, Souprava>();

            Souprava s = new Souprava(this, "A", true, 0.1f, 40, seznamStanic["ANem"]);
            seznamSouprav.Add("A1", s);
            s = new Souprava(this, "B", true, 0.1f, 40, seznamStanic["BČer"]);
            seznamSouprav.Add("B1", s);
            s = new Souprava(this, "C", true, 0.1f, 40, seznamStanic["CHáj"]);
            seznamSouprav.Add("C1", s);
        }

        private void najdiSousedy()
        {
            foreach(KeyValuePair<string, Stanice> k in seznamStanic)
            {
                k.Value.najdiSousedy(seznamStanic);
            }
        }

        public SortedList<string, Stanice> getSeznamStanic()
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
            while (!kalendar.jePrazdny())
            {
                Udalost zpracovavanaUdalost = kalendar.vratNejaktualnejsi();
                cas = zpracovavanaUdalost.kdy;
                (zpracovavanaUdalost.kdo).zpracuj(zpracovavanaUdalost);
                cas++;
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
