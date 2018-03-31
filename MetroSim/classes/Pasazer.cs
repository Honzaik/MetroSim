using System;
using System.Collections.Generic;

namespace MetroSim
{
    class Pasazer : Proces
    {
        public Stanice sZacatek;
        public Stanice sKonec;
        private Stanice aktualniStanice;
        private Stanice pristiStanice;
        public int start;

        public Pasazer(Model model, string id, Stanice sZacatek, Stanice sKonec, int start)
        {
            this.model = model;
            this.id = id;
            this.sZacatek = sZacatek;
            this.sKonec = sKonec;
            this.start = start;
            this.aktualniStanice = sZacatek;
            //Console.WriteLine("vytvoren pasazer " + id + ", jede z " + sZacatek.getComboBoxName() + " do " + sKonec.getComboBoxName() + ", vyrazi v case " + start);
        }

        public void setPristiStanice()
        {
            if(aktualniStanice.jePrestupni && aktualniStanice.prestupniPismeno.Equals(sKonec.pismeno)) //jsem v prestupni stanici a chci prestupit
            {
                pristiStanice = model.getSeznamStanic().stanice[sKonec.pismeno + aktualniStanice.jmeno.Substring(0, 3)];
                return;
            }

            if (aktualniStanice.pismeno.Equals(sKonec.pismeno)) //jsou jiz na stejne lince
            {
                pristiStanice = sKonec;
            }
            else
            {
                foreach (KeyValuePair<string, Stanice> k in model.getSeznamStanic().stanice) //najdi prestupni stanici ktera je
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
            return pristiStanice;
        }

        public Stanice getAktualniStanice()
        {
            return aktualniStanice;
        }

        public override void zpracuj(Udalost u)
        {
            switch (u.co)
            {
                case TypUdalosti.prichodDoStanice:
                    if (this.id.Equals("0")) {
                        Console.WriteLine("pasazer " + id + " prisel do stanice " + aktualniStanice.id + " v " + u.kdy);
                    }
                    if(aktualniStanice == sKonec)
                    {
                        //Console.WriteLine("PASAZER " + id + " DORAZIL DO KONCE " + aktualniStanice.id + " v " + u.kdy);
                        if (this.id.Equals("0")) //hlavni pasazer
                        {
                            model.jeKonec = true;
                        }
                        model.removePasazer(this);
                        break;
                    }
                    setPristiStanice();
                    if(aktualniStanice.jePrestupni && aktualniStanice.jmeno.Equals(pristiStanice.jmeno)) //prestoupeni
                    {
                        model.pridejDoKalendare(new Udalost(u.kdy + aktualniStanice.dobaPrestupu, this, TypUdalosti.prichodDoStanice));
                    }
                    else
                    {
                        model.getSeznamStanic().stanice[aktualniStanice.id].zaradNaNastupiste(this, pristiStanice);
                    }
                    aktualniStanice = pristiStanice;
                    break;
            }
        }
    }
}
