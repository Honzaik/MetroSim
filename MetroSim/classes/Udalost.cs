﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetroSim
{
    class Udalost
    {
        public int kdy;
        public Proces kdo;
        public TypUdalosti co;

        public Udalost(int kdy, Proces kdo, TypUdalosti co)
        {
            this.kdy = kdy;
            this.kdo = kdo;
            this.co = co;
        }
    }
}
