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
        public int dobaPrestupu;

        private Queue<Pasazer> nastupisteVice; //fronta lidi, kteří chtějí je směrem více
        private Queue<Pasazer> nastupisteMene; //-|- mene
        private List<Stanice> sousedi; // index = 0 - pravy soused (vetsi km); index = 1 - levy soused, index = 2 - prestupni

        public Stanice(string id, string pismeno, string jmeno, float kilometr, bool jeKonecna, bool jePrestupni, string prestupniPismeno, int dobaPrestupu)
        {
            this.id = id;
            this.pismeno = pismeno;
            this.jmeno = jmeno;
            this.kilometr = kilometr;
            this.jeKonecna = jeKonecna;
            this.jePrestupni = jePrestupni;
            this.prestupniPismeno = prestupniPismeno;
            this.dobaPrestupu = dobaPrestupu;
            sousedi = new List<Stanice>();
            nastupisteVice = new Queue<Pasazer>(); //smer vice kilometru
            nastupisteMene = new Queue<Pasazer>(); //smer mene kilometru
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


        public float vzdalenostOdSouseda(int index) // index = 0 - pravy soused (vetsi km); index = 1 - levy soused, index = 2 - prestupni
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
            Console.WriteLine("----Sousedi " + id + "-----");
            for (int i = 0; i < sousedi.Count; i++)
            {
                Console.WriteLine(sousedi[i].jmeno + "(" + sousedi[i].id + ") vzdalenost: " + vzdalenostOdSouseda(i));
            }
            Console.WriteLine("---------");
        }

        public void najdiSousedy(SortedList<string, Stanice> seznamStanic)
        {
            Stanice novySoused = null;
            if (jeKonecna) //hledam pouze jednu nejblizsi stanici
            {
                foreach (KeyValuePair<string, Stanice> k in seznamStanic)
                {
                    //je na stejné lince && není to ona sama && ještě jsem jí nenašel && (ještě jsem žádného kandidáta nenašel || našel jsem blížší než je aktualne novySoused) 
                    if ((k.Value.pismeno.Equals(pismeno)) && (!k.Value.id.Equals(id)) && (!jeSoused(k.Value)) &&
                        (novySoused == null || (Math.Abs(kilometr - k.Value.kilometr) < Math.Abs(kilometr - novySoused.kilometr))))
                    {
                            novySoused = k.Value;
                    }
                }
                pridejSouseda(novySoused);
            }
            else
            {
                //"normální stanice" má 2 sousedy - 2x foreach, jelikož jeden dělal problémy a toto není až taková žátěž
                novySoused = null;
                //hledam souseda na index 0, to je ten s nejmensim kladnym rozdilem
                foreach (KeyValuePair<string, Stanice> k in seznamStanic)
                {
                    float novyRozdil = k.Value.kilometr - kilometr;
                    if (novyRozdil > 0 && (k.Value.pismeno.Equals(pismeno)) && (!k.Value.id.Equals(id)) && (!jeSoused(k.Value)) &&
                        (novySoused == null || (novyRozdil < (novySoused.kilometr - kilometr))))
                    {
                        novySoused = k.Value;
                    }
                }
                pridejSouseda(novySoused);

                novySoused = null;
                //hledam souseda na index 1, to je ten s nejmensim zapornym rozdilem
                foreach (KeyValuePair<string, Stanice> k in seznamStanic)
                {
                    float novyRozdil = kilometr - k.Value.kilometr;
                    if (novyRozdil > 0 && (k.Value.pismeno.Equals(pismeno)) && (!k.Value.id.Equals(id)) && (!jeSoused(k.Value)) &&
                       (novySoused == null || (novyRozdil < (kilometr - novySoused.kilometr))))
                    {
                        novySoused = k.Value;
                    }
                }
                pridejSouseda(novySoused);
            }

            if (jePrestupni) //stanice je přestupní tak ještě najdeme její adékvátní stanici na druhé lince (předpokládá se, že z jedné stanice jde pouze přestoupit na jednu linku)
            {
                novySoused = null;
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
            if (pismeno.Equals(pristi.pismeno))
            {
                if(pristi.kilometr > kilometr)
                {
                    nastupisteVice.Enqueue(p);
                    //Console.WriteLine("pasazer " + p.id + " zařazen do fronty VICE (" + id + "), jede do stanice " + pristi.id);
                }
                else
                {
                    nastupisteMene.Enqueue(p);
                    //Console.WriteLine("pasazer " + p.id + " zařazen do fronty MENE (" + id + "), jede do stanice " + pristi.id);
                }
            }
            else
            {
                Console.WriteLine("CHYBAAA");
                System.Environment.Exit(1);
            }
        }

        public Pasazer vratPasazeraVeSmeru(bool smerVice)
        {
            Pasazer p = null;
            if(smerVice)
            {
                if(nastupisteVice.Count > 0)
                {
                    p = nastupisteVice.Dequeue();
                }
            }
            else
            {
                if(nastupisteMene.Count > 0)
                {
                    p = nastupisteMene.Dequeue();
                }
            }
            return p;
        }

        public void reset()
        {
            nastupisteVice = new Queue<Pasazer>(); //smer vice kilometru
            nastupisteMene = new Queue<Pasazer>(); //smer mene kilometru
        }
    }
}
