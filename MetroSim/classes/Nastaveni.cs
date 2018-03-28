using System.Collections.Generic;

namespace MetroSim
{
    class Nastaveni
    {

        public SortedList<string, NastaveniLinky> nastaveniLinek;
        public int casPrichodu;
        public Stanice pocatecniStanice;
        public Stanice konecnaStanice;
        public int frekvenceLidi;

        public Nastaveni()
        {
            pocatecniStanice = null;
            konecnaStanice = null;
            casPrichodu = 0;
            nastaveniLinek = new SortedList<string, NastaveniLinky>();
            frekvenceLidi = 1; //default
        }

        public void pridejNastaveniLinky(NastaveniLinky n)
        {
            nastaveniLinek.Add(n.linka, n);
        }


    }
}
