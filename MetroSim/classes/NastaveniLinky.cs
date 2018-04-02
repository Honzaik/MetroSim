namespace MetroSim
{
    class NastaveniLinky
    {
        public string linka; //písmeno linky
        public int pocetSouprav; 
        public float rychlostSouprav;
        public int kapacitaSouprav;
        public float dobaCekaniVeStanici; //jak dlouho každá souprava bude čekat ve stanici po příjezdu
        

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
