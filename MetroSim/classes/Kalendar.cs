using System;
using System.Collections.Generic;

namespace MetroSim
{
    class Kalendar
    {
        private List<Udalost> seznamUdalosti;

        public Kalendar()
        {
            seznamUdalosti = new List<Udalost>();
        }

        public void pridejUdalost(Udalost u)
        {
            seznamUdalosti.Add(u);
        }

        public Udalost vratNejaktualnejsi()
        {
            if (seznamUdalosti.Count > 0)
            {
                Udalost nejaktualnejsi = seznamUdalosti[0];
                for(int i = 1; i < seznamUdalosti.Count; i++)
                {
                    if (seznamUdalosti[i].kdy < nejaktualnejsi.kdy)
                    {
                        nejaktualnejsi = seznamUdalosti[i];
                    }
                }
                seznamUdalosti.Remove(nejaktualnejsi);
                return nejaktualnejsi;
            }
            else
            {
                return null;
            }
           
        }

        public Boolean jePrazdny()
        {
            return (seznamUdalosti.Count > 0) ? false : true;
        }
    }
}
