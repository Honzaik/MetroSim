using System;
using System.Collections.Generic;

namespace MetroSim
{
    class Pasazer : Proces
    {
        public Stanice sZacatek;
        public Stanice sKonec;
        public Stanice aktualniStanice;
        public Stanice pristiStanice;
        public int start;

        public Pasazer(Model model, string id, Stanice sZacatek, Stanice sKonec, int start)
        {
            this.model = model;
            this.id = id;
            this.sZacatek = sZacatek;
            this.sKonec = sKonec;
            this.start = start;
            this.aktualniStanice = sZacatek;
            Console.WriteLine("vytvoren pasazer " + id + ", jede z " + sZacatek.getComboBoxName() + " do " + sKonec.getComboBoxName() + ", vyrazib v case " + start);
        }

        public void setPristiStanice()
        {
            if (aktualniStanice.pismeno.Equals(sKonec.pismeno)) //jsou jiz na stejne lince
            {
                pristiStanice = sKonec;
            }
            else
            {
                foreach (KeyValuePair<string, Stanice> k in model.getSeznamStanic()) //najdi prestupni stanici ktera je
                {
                    if (k.Value.jePrestupni && k.Value.pismeno.Equals(aktualniStanice.pismeno) && k.Value.prestupniPismeno.Equals(sKonec.pismeno))
                    {
                        pristiStanice = k.Value;
                    }
                }
            }
        }

        public Stanice getPristiStanice()
        {
            Stanice pristi = null;
            if (aktualniStanice.pismeno.Equals(sKonec.pismeno)) //jsou jiz na stejne lince
            {
                pristi = sKonec;
            }
            else
            {
                foreach (KeyValuePair<string, Stanice> k in model.getSeznamStanic()) //najdi prestupni stanici ktera je
                {
                    if (k.Value.jePrestupni && k.Value.pismeno.Equals(aktualniStanice.pismeno) && k.Value.prestupniPismeno.Equals(sKonec.pismeno))
                    {
                        pristi = k.Value;
                    }
                }
            }
            return pristi;
        }

        public override void zpracuj(Udalost u)
        {
            switch (u.co)
            {
                case TypUdalosti.prichodDoStanice:
                    setPristiStanice();
                    model.getSeznamStanic()[aktualniStanice.id].zaradNaNastupiste(this, pristiStanice);
                    break;
            }
        }
    }
}
