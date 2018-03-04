namespace MetroSim
{
    class NastaveniLinky
    {
        public string linka;
        public int pocetSouprav;
        public float rychlostSouprav;
        public int kapacitaSouprav;
        public int dobaCekaniVeStanici;
        

        public NastaveniLinky(string linka, int pocetSouprav, float rychlostSouprav, int kapacitaSouprav, int dobaCekaniVeStanici)
        {
            this.linka = linka;
            this.pocetSouprav = pocetSouprav;
            this.rychlostSouprav = rychlostSouprav;
            this.kapacitaSouprav = kapacitaSouprav;
            this.dobaCekaniVeStanici = dobaCekaniVeStanici;
        }
    }
}
