using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MetroSim.classes
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
            s.Start();
            najdiSousedy();
            s.Stop();
            Console.WriteLine("Načteni sousedé za " + s.ElapsedMilliseconds + " ms");

            Stanice stanice1 = seznamStanic[9];
            Console.WriteLine(stanice1.id + " " + stanice1.jmeno + " pocet: " + stanice1.pocetSousedu());
            stanice1.vypisSousedy();
        }

        private void najdiSousedy()
        {
            foreach(Stanice stanice in seznamStanic)
            {
                int pocetSousedu = 2;
                if (stanice.jeKonecna)
                {
                    pocetSousedu--;
                }
                for(int i = 0; i < pocetSousedu; i++)
                {
                    Stanice novySoused = najdiNovehoSouseda(stanice, false);
                    stanice.pridejSouseda(novySoused);
                }
                if (stanice.jePrestupni)
                {
                    Stanice novySoused = najdiNovehoSouseda(stanice, true);
                    stanice.pridejSouseda(novySoused);
                }
            }
        }

        private Stanice najdiNovehoSouseda(Stanice stanice, bool prestupni)
        {
            Stanice novySoused = null;
            foreach(Stanice s in seznamStanic)
            {
                if (!prestupni)
                {
                    if ((s.pismeno == stanice.pismeno) &&
                        (s.id != stanice.id) &&
                        (!stanice.jeSoused(s)) &&
                        (novySoused == null || (Math.Abs(stanice.kilometr - s.kilometr) < Math.Abs(stanice.kilometr - novySoused.kilometr))))
                    {
                        novySoused = s;
                    }
                }
                else
                {
                    if((s.id != stanice.id) && s.jmeno == stanice.jmeno)
                    {
                        novySoused = s;
                    }
                }
            }
            return novySoused;
        }

        public List<Stanice> getSeznamStanic()
        {
            return seznamStanic;
        }

    }
}
