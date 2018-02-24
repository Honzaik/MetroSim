using System;
using System.Collections.Generic;
using System.Linq;

namespace MetroSim
{
    class Stanice
    {

        public string id;
        public string jmeno;
        public bool jeKonecna;
        public bool jePrestupni;
        public float kilometr;
        public string pismeno;

        private List<Stanice> sousedi;

        public Stanice(string id, string pismeno, string jmeno, float kilometr, bool jePrestupni, bool jeKonecna)
        {
            this.id = id;
            this.pismeno = pismeno;
            this.jmeno = jmeno;
            this.kilometr = kilometr;
            this.jePrestupni = jePrestupni;
            this.jeKonecna = jeKonecna;
            sousedi = new List<Stanice>();
        }

        public void pridejSouseda(Stanice soused)
        {
            sousedi.Add(soused);
        }

        public Stanice vratSouseda(int i)
        {
            return sousedi.ElementAt(i);
        }

        public int pocetSousedu()
        {
            return sousedi.Count;
        }

        public Boolean jeSoused(Stanice stanice)
        {
            return (sousedi.IndexOf(stanice) > -1);
        }


        public float vzdalenostOdSouseda(int index) // index = 0 - levý soused (mensi km); index = 1 - pravý soused, index = 2 - prestupni
        {
            Stanice soused = sousedi.ElementAt(index);
            if (soused.jmeno == jmeno)
            {
                return 0;
            }
            else
            {
                return (float)Math.Round(Math.Abs(soused.kilometr - kilometr), 1);
            }
        }

        public void vypisSousedy()
        {
            Console.WriteLine("---------");
            for (int i = 0; i < sousedi.Count; i++)
            {
                Console.WriteLine(sousedi[i].jmeno + "(" + sousedi[i].id + ") vzdalenost: " + vzdalenostOdSouseda(i));
            }
            Console.WriteLine("---------");
        }

        public void najdiSousedy(List<Stanice> seznamStanic)
        {
            int pocetSousedu = 2;
            if (jeKonecna)
            {
                pocetSousedu--;
            }
            for (int i = 0; i < pocetSousedu; i++)
            {
                Stanice novySoused = null;
                foreach (Stanice s in seznamStanic)
                {
                    if ((s.pismeno == pismeno) &&
                        (s.id != id) &&
                        (!jeSoused(s)) &&
                        (novySoused == null || (Math.Abs(kilometr - s.kilometr) < Math.Abs(kilometr - novySoused.kilometr))))
                    {
                        novySoused = s;
                    }
                }
                pridejSouseda(novySoused);
            }
            if (jePrestupni)
            {
                Stanice novySoused = null;
                foreach (Stanice s in seznamStanic)
                {
                    if ((s.id != id) && s.jmeno == jmeno)
                    {
                        novySoused = s;
                    }
                }
                pridejSouseda(novySoused);
            }
        }


    }
}
