using System.Collections.Generic;

namespace MetroSim
{
    class Nastaveni
    {

        public SortedList<string, NastaveniLinky> nastaveniLinek;
        public int pocatecniCas;
        public Stanice pocatecniStanice;
        public Stanice konecnaStanice;

        public Nastaveni()
        {
            pocatecniStanice = null;
            konecnaStanice = null;
            pocatecniCas = 0;
            nastaveniLinek = new SortedList<string, NastaveniLinky>();
        }

        public void pridejNastaveniLinky(NastaveniLinky n)
        {
            nastaveniLinek.Add(n.linka, n);
        }


    }
}
