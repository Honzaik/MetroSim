using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MetroSim
{
    class Model
    {

        private SortedList<string, Stanice> seznamStanic;
        private SortedList<int, Pasazer> seznamPasazeru;
        private string settingsPath = "files/stanice.txt"; 

        public Model()
        {

        }

        public Model(string settingsPath)
        {
            this.settingsPath = settingsPath;
        }

        public void init()
        {
            seznamStanic = SettingsLoader.nactiNastaveni(settingsPath);
            Console.WriteLine("Načteno " + seznamStanic.Count + " stanic");

            najdiSousedy();

            Stanice stanice1 = seznamStanic[9];
            Console.WriteLine(stanice1.id + " " + stanice1.jmeno + " pocet: " + stanice1.pocetSousedu());
            stanice1.vypisSousedy();

            seznamPasazeru = new SortedList<int, Pasazer>();
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
            Pasazer hlavni = new Pasazer(0, zacatek, konec, start);
            seznamPasazeru.Add(seznamPasazeru.Count, hlavni);
        }
    }
}
