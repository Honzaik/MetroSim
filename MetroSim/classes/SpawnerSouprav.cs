﻿using System;

namespace MetroSim
{
    class SpawnerSouprav : Proces
    {

        public SpawnerSouprav(Model model, string id)
        {
            this.model = model;
            this.id = id;
        }

        public override void zpracuj(Udalost udalost)
        {
            switch (udalost.co)
            {
                case TypUdalosti.spawnSouprav:
                    //Console.WriteLine("VOLAM SPAWN " + u.kdy);
                    model.spawniCastSouprav();
                    break;
            }
        }
    }
}
