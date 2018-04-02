using System.Collections.Generic;

namespace MetroSim
{
    class Nastaveni
    {

        public SortedList<string, NastaveniLinky> nastaveniLinek; //seznam nastavení specifických pro každou linku
        public float casPrichodu; //čas příchodu hlavního pasažéra
        public Stanice pocatecniStanice; //počáteční stanice hlavního pasažéra
        public Stanice konecnaStanice; //konečná stanice hlavního pasažéra
        public int frekvenceLidi; //kolik lidí se má spawnout za 1 min

        public Nastaveni()
        {
            pocatecniStanice = null;
            konecnaStanice = null;
            casPrichodu = 0;
            nastaveniLinek = new SortedList<string, NastaveniLinky>();
            frekvenceLidi = 1; //tyto hodnoty se stejne updatuji z GUI, jen inicializace
        }

        public void pridejNastaveniLinky(NastaveniLinky n)
        {
            nastaveniLinek.Add(n.linka, n);
        }


    }
}
