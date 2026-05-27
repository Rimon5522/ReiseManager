namespace ReiseProgramm
{
    class Wetterbericht
    {
        private string[] wetterArten =
        {
            "Sonnig",
            "Regnerisch",
            "Windig",
            "Warm",
            "Leicht bewölkt"
        };

        public void WetterAnzeigen(Random random, DateTime vonDatum, DateTime bisDatum)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("\n=================================================");
            Console.WriteLine("                 WETTERBERICHT");
            Console.WriteLine("=================================================");

            Console.ResetColor();

            DateTime aktuellerTag = vonDatum;

            while (aktuellerTag <= bisDatum)
            {
                int temperatur = random.Next(15, 35);

                string wetter = wetterArten[random.Next(wetterArten.Length)];

                Console.WriteLine(
                    $"{aktuellerTag,-25:dddd, dd.MM.yyyy} {temperatur,-5}°C | {wetter}"
                );

                aktuellerTag = aktuellerTag.AddDays(1);
            }
        }
    }
}