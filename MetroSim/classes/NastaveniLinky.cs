namespace MetroSim
{
    class NastaveniLinky
    {
        public string linka;
        public int pocetSouprav;
        public float rychlostSouprav;
        public int kapacitaSouprav;
        public float dobaCekaniVeStanici;
        

        public NastaveniLinky(string linka, int pocetSouprav, float rychlostSouprav, int kapacitaSouprav, float dobaCekaniVeStanici)
        {
            this.linka = linka;
            this.pocetSouprav = pocetSouprav;
            this.rychlostSouprav = rychlostSouprav;
            this.kapacitaSouprav = kapacitaSouprav;
            this.dobaCekaniVeStanici = dobaCekaniVeStanici;
        }
    }
}
