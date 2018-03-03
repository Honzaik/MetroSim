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
        private int dobaCekaniVeStanici;

        public Souprava(Model model, string id, string pismeno, bool smerVice, float rychlost, int kapacita, int dobaCekaniVeStanici, Stanice pocatecniStanice)
        {
            this.model = model;
            this.id = id;
            this.pismeno = pismeno;
            this.smerVice = smerVice;
            this.rychlost = rychlost;
            this.kapacita = kapacita;
            this.dobaCekaniVeStanici = dobaCekaniVeStanici;
            this.aktualniStanice = pocatecniStanice;
        }

        public void jedDoDalsiStanice()
        {
            float vzdalenostPristiStanice = 0f;
            int pristiStaniceIndex = 0;
            if(aktualniStanice.pocetSousedu() > 1 && !smerVice)
            {
                pristiStaniceIndex = 1;
            }

            vzdalenostPristiStanice = aktualniStanice.vzdalenostOdSouseda(pristiStaniceIndex);

            if (pismeno == "A")
            {
                Console.WriteLine("souprava: " + id + " | čas: " + model.getCas() + " | vzdalenost do pristi z " + aktualniStanice.id + " smer " + smerVice + " je " + vzdalenostPristiStanice + " i: " + pristiStaniceIndex);
            }
            int casPrijezdu = model.getCas() + (int)(vzdalenostPristiStanice / rychlost);
            model.pridejDoKalendare(new Udalost(casPrijezdu, this, TypUdalosti.prijezdDoStanice));
            if (aktualniStanice.jeKonecna)
            {
                if(aktualniStanice.kilometr > 0)
                {
                    smerVice = false;
                }
                else
                {
                    smerVice = true;
                }
            }
            aktualniStanice = aktualniStanice.vratSouseda(pristiStaniceIndex);
            if (pismeno == "A")
            {
                //aktualniStanice.vypisSousedy();
            }
        }

        public override void zpracuj(Udalost u)
        {
            switch (u.co)
            {
                case TypUdalosti.prijezdDoStanice:
                    if(aktualniStanice.pismeno == "A")
                    {
                        Console.WriteLine("souprava " + id + " prijeda do stanice " + aktualniStanice.id + " cas " + u.kdy);
                    } 
                    model.pridejDoKalendare(new Udalost(u.kdy + dobaCekaniVeStanici, this, TypUdalosti.vyjezdZeStanice));
                    break;
                case TypUdalosti.vyjezdZeStanice:
                    jedDoDalsiStanice();
                    break;
            }
        }
    }
}
