namespace MetroSim
{
    class NastaveniLinky
    {
        public string linka;
        public int pocetSouprav;
        public float rychlostSouprav;
        public int kapacitaSouprav;
        

        public NastaveniLinky(string linka, int pocetSouprav, float rychlostSouprav, int kapacitaSouprav)
        {
            this.linka = linka;
            this.pocetSouprav = pocetSouprav;
            this.rychlostSouprav = rychlostSouprav;
            this.kapacitaSouprav = kapacitaSouprav;
        }
    }
}
