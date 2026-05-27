namespace ReiseProgramm
{
    class Reise
    {
        public string Land { get; set; }
        public double PreisProPerson { get; set; }

        public Reise(string land, double preis)
        {
            Land = land;
            PreisProPerson = preis;
        }
    }
}
