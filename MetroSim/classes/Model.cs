using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MetroSim
{
    class Model
    {

        private List<Stanice> seznamStanic;
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
            Stopwatch s = new Stopwatch();

            najdiSousedy();

            Stanice stanice1 = seznamStanic[9];
            Console.WriteLine(stanice1.id + " " + stanice1.jmeno + " pocet: " + stanice1.pocetSousedu());
            stanice1.vypisSousedy();
        }

        private void najdiSousedy()
        {
            foreach(Stanice s in seznamStanic)
            {
                s.najdiSousedy(seznamStanic);
            }
        }



        public List<Stanice> getSeznamStanic()
        {
            return seznamStanic;
        }

    }
}
