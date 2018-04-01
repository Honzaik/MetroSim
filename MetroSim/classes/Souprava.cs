using System;
using System.Collections.Generic;

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
        private float dobaCekaniVeStanici;

        public Souprava(Model model, string id, string pismeno, bool smerVice, float rychlost, int kapacita, float dobaCekaniVeStanici, Stanice pocatecniStanice)
        {
            this.model = model;
            this.id = id;
            this.pismeno = pismeno;
            this.smerVice = smerVice;
            this.rychlost = rychlost;
            this.kapacita = kapacita;
            this.dobaCekaniVeStanici = dobaCekaniVeStanici;
            aktualniStanice = pocatecniStanice;
            seznamPasazeru = new List<Pasazer>();
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

            float casPrijezdu = model.getCas() + (vzdalenostPristiStanice / rychlost);
            model.pridejDoKalendare(new Udalost(casPrijezdu, this, TypUdalosti.prijezdDoStanice));
            if (aktualniStanice.jeKonecna)
            {
                bool oldSmer = smerVice;
                if(aktualniStanice.kilometr > 0)
                {
                    smerVice = false;
                }
                else
                {
                    smerVice = true;
                }
                if(oldSmer ^ smerVice)
                {
                    //Console.WriteLine("OTACIM SE " + id);
                }
            }
            aktualniStanice = aktualniStanice.vratSouseda(pristiStaniceIndex);
        }

        public override void zpracuj(Udalost udalost)
        {
            switch (udalost.co)
            {
                case TypUdalosti.prijezdDoStanice:
                    //if(pismeno == "A") Console.WriteLine("souprava " + id + " prijela do stanice " + aktualniStanice.jmeno + " (" + aktualniStanice.pismeno + ") cas " + u.kdy);
                    vystupovani();    
                    model.pridejDoKalendare(new Udalost(udalost.kdy + dobaCekaniVeStanici, this, TypUdalosti.vyjezdZeStanice));

                    break;
                case TypUdalosti.vyjezdZeStanice:
                    nastupovani();
                    jedDoDalsiStanice();
                    break;
            }
        }

        private void vystupovani()
        {
            Pasazer p = null;
            for(int i = 0; i < seznamPasazeru.Count; i++)
            {
                p = seznamPasazeru[i];
                if (p.getPristiStanice() == aktualniStanice)
                {
                    //Console.WriteLine("pasazer " + p.id + " vystupuje v " + model.getCas() + " ve stanici " + aktualniStanice.id + " z " + id);
                    model.pridejDoKalendare(new Udalost(model.getCas(), p, TypUdalosti.prichodDoStanice));
                    seznamPasazeru.Remove(p);
                }
                else
                {
                    //Console.WriteLine("pasazer nevystupuje protoze akt stanice je " + aktualniStanice.id + " ale chce do " + p.getPristiStanice().id);
                }
            }
        }

        private void nastupovani()
        {
            Pasazer p = null;
            while((seznamPasazeru.Count < kapacita) && (p = aktualniStanice.vratPasazeraVeSmeru(smerVice)) != null)
            {
                //Console.WriteLine("pasazer " + p.id + " nastupuje v " + model.getCas() + " ve stanici " + aktualniStanice.id + " do " + id);
                seznamPasazeru.Add(p);
            }
        }
    }
}
