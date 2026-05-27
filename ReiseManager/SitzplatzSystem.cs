namespace ReiseProgramm
{
    class SitzplatzSystem
    {
        public string[] Sitzplaetze =
        {
            "A1", "A2", "A3",
            "B1", "B2", "B3",
            "C1", "C2", "C3",
            "D1"
        };

        public List<string> BelegtePlaetze = new List<string>();

        public List<string> FreiePlaetze = new List<string>();

        public void FreiePlaetzeAnzeigen()
        {
            FreiePlaetze.Clear();

            foreach (string platz in Sitzplaetze)
            {
                if (!BelegtePlaetze.Contains(platz))
                {
                    FreiePlaetze.Add(platz);
                }
            }

            Console.WriteLine("\nFreie Sitzplätze:");

            for (int i = 0; i < FreiePlaetze.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {FreiePlaetze[i]}");
            }
        }
    }
}