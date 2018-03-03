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
        public string prestupniPismeno;

        private Queue<Pasazer> nastupiste1;
        private Queue<Pasazer> nastupiste2;
        private List<Stanice> sousedi;

        public Stanice(string id, string pismeno, string jmeno, float kilometr, bool jeKonecna, bool jePrestupni, string prestupniPismeno)
        {
            this.id = id;
            this.pismeno = pismeno;
            this.jmeno = jmeno;
            this.kilometr = kilometr;
            this.jeKonecna = jeKonecna;
            this.jePrestupni = jePrestupni;
            this.prestupniPismeno = prestupniPismeno;
            sousedi = new List<Stanice>();
            nastupiste1 = new Queue<Pasazer>();
            nastupiste2 = new Queue<Pasazer>();
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

        public string getComboBoxName()
        {
            return jmeno + " (" + pismeno + ")";
        }


        public float vzdalenostOdSouseda(int index) // index = 0 - levý soused (mensi km); index = 1 - pravý soused, index = 2 - prestupni
        {
            Stanice soused = sousedi.ElementAt(index);
            if (soused.jmeno.Equals(jmeno))
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

        public void najdiSousedy(SortedList<string, Stanice> seznamStanic)
        {
            int pocetSousedu = 2;
            if (jeKonecna)
            {
                pocetSousedu--;
            }
            for (int i = 0; i < pocetSousedu; i++)
            {
                Stanice novySoused = null;
                foreach (KeyValuePair<string, Stanice> k in seznamStanic)
                {
                    if ((k.Value.pismeno.Equals(pismeno)) &&
                        (!k.Value.id.Equals(id)) &&
                        (!jeSoused(k.Value)) &&
                        (novySoused == null || (Math.Abs(kilometr - k.Value.kilometr) < Math.Abs(kilometr - novySoused.kilometr))))
                    {
                        novySoused = k.Value;
                    }
                }
                pridejSouseda(novySoused);
            }
            if (jePrestupni)
            {
                Stanice novySoused = null;
                foreach (KeyValuePair<string, Stanice> k in seznamStanic)
                {
                    if ((!k.Value.id.Equals(id)) && k.Value.jmeno.Equals(jmeno))
                    {
                        novySoused = k.Value;
                    }
                }
                pridejSouseda(novySoused);
            }
        }

        public void zaradNaNastupiste(Pasazer p, Stanice pristi)
        {
            Console.WriteLine("pasazer " + p.id + " zařazen do fronty, jede do stanice " + pristi.id);
        }
    }
}
