using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroSim
{
    abstract class Proces
    {
        public string id;
        protected Model model;
        public abstract void zpracuj(Udalost u);
    }
}
