using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void odeberUdalost(int kdy, Proces kdo, TypUdalosti co)
        {
            foreach(Udalost u in seznamUdalosti)
            {
                if(u.kdy == kdy && u.kdo == kdo && u.co == co)
                {
                    seznamUdalosti.Remove(u);
                    break;
                }
            }
        }

        public Udalost vratNejaktualnejsi()
        {
            Udalost nejaktualnejsi = null;
            foreach(Udalost u in seznamUdalosti)
            {
                if(nejaktualnejsi == null)
                {
                    nejaktualnejsi = u;
                }
                else
                {
                    if(u.kdy < nejaktualnejsi.kdy)
                    {
                        nejaktualnejsi = u;
                    }
                }
            }
            seznamUdalosti.Remove(nejaktualnejsi);
            return nejaktualnejsi;
        }

        public Boolean jePrazdny()
        {
            return (seznamUdalosti.Count > 0) ? false : true;
        }
        
    }
}
