using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroSim
{
    class Souprava : Proces
    {
        public string pismeno;
        public bool smerVice;
        private float rychlost;
        private int kapacita;
        private Stanice aktualniStanice;
        private List<Pasazer> seznamPasazeru;

        public Souprava(Model model, string pismeno, bool smerVice, float rychlost, int kapacita, Stanice pocatecniStanice)
        {
            this.model = model;
            this.pismeno = pismeno;
            this.smerVice = smerVice;
            this.rychlost = rychlost;
            this.kapacita = kapacita;
            this.aktualniStanice = pocatecniStanice;
        }

        public void jedDoDalsiStanice()
        {
            int casPrijezdu = model.getCas() + (int)(aktualniStanice.vzdalenostOdSouseda(0) / rychlost);
            model.pridejDoKalendare(new Udalost(casPrijezdu, this, TypUdalosti.prijezdDoStanice));
            aktualniStanice = aktualniStanice.vratSouseda(0);
        }

        public override void zpracuj(Udalost u)
        {
            switch (u.co)
            {
                case TypUdalosti.prijezdDoStanice:
                    Console.WriteLine("souprava " + id + " prijeda do stanice " + aktualniStanice.id + " cas " + u.kdy);
                    model.pridejDoKalendare(new Udalost(u.kdy + 5, this, TypUdalosti.vyjezdZeStanice));
                    break;
                case TypUdalosti.vyjezdZeStanice:
                    jedDoDalsiStanice();
                    break;
            }
        }
    }
}
