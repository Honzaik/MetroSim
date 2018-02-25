using System;
using System.Collections.Generic;

namespace MetroSim
{
    class Pasazer
    {
        public int id;
        public Stanice sZacatek;
        public Stanice sKonec;
        public Stanice aktualniStanice;
        public Stanice pristiStanice;
        public int start;

        public Pasazer(int id, Stanice sZacatek, Stanice sKonec, int start)
        {
            this.id = id;
            this.sZacatek = sZacatek;
            this.sKonec = sKonec;
            this.start = start;
            this.aktualniStanice = sZacatek;
            Console.WriteLine("vytvoren pasazer " + id + ", jede z " + sZacatek.getComboBoxName() + " do " + sKonec.getComboBoxName() + ", vyrazib v case " + start);
        }

        public void setPristiStanice(SortedList<string, Stanice> seznamStanic)
        {
            if(aktualniStanice.pismeno.Equals(sKonec.pismeno)) //jsou jiz na stejne lince
            {
                pristiStanice = sKonec;
            }
            else
            {
                foreach(KeyValuePair<string, Stanice> k in seznamStanic) //najdi prestupni stanici ktera je
                {
                    if(k.Value.jePrestupni && k.Value.pismeno.Equals(aktualniStanice.pismeno) && k.Value.prestupniPismeno.Equals(sKonec.pismeno))
                    {
                        pristiStanice = k.Value;
                    }
                }
            }
        }

    }
}
