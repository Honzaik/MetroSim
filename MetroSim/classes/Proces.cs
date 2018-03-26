namespace MetroSim
{
    abstract class Proces
    {
        public string id;
        protected Model model;
        public abstract void zpracuj(Udalost u);
    }
}
