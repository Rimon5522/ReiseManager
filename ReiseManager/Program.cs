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

    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            string password = "5522";
            int versuche = 3;
            bool zugang = false;

            while (versuche > 0)
            {
                Console.Write("Bitte gib dein Passwort ein: ");
                string eingabe = Console.ReadLine();

                if (eingabe == password)
                {
                    zugang = true;
                    Console.WriteLine("Zugang gewährt!\n");
                    break;
                }
                else
                {
                    versuche--;

                    if (versuche > 0)
                    {
                        Console.WriteLine($"Passwort falsch! Noch {versuche} Versuche.\n");
                    }
                    else
                    {
                        Console.WriteLine("Konto gesperrt!");
                    }
                }
            }

            if (!zugang)
            {
                Console.ReadKey();
                return;
            }

            string firstName, lastName, age, email;

            Console.Write("Hallo, bitte gib deinen Vornamen ein: ");
            firstName = Console.ReadLine() ?? "";

            Console.Write("Gib deinen Nachnamen ein: ");
            lastName = Console.ReadLine() ?? "";

            Console.Write("Gib dein Alter ein: ");
            age = Console.ReadLine() ?? "";

            Console.Write("Gib deine E-Mail ein: ");
            email = Console.ReadLine() ?? "";

            Console.WriteLine("\n==============================");
            Console.WriteLine("       DEINE DATEN");
            Console.WriteLine("==============================");

            Console.WriteLine($"Vorname: {firstName}");
            Console.WriteLine($"Nachname: {lastName}");
            Console.WriteLine($"Alter: {age}");
            Console.WriteLine($"E-Mail: {email}");

            List<Reise> reisen = new List<Reise>()
            {
                new Reise("Spanien", 600),
                new Reise("Italien", 750),
                new Reise("Japan", 1000)
            };

            Console.WriteLine("\n==============================");
            Console.WriteLine("       REISEBUCHUNG");
            Console.WriteLine("==============================");

            for (int i = 0; i < reisen.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {reisen[i].Land} - {reisen[i].PreisProPerson} Euro");
            }

            int auswahl;

            Console.Write("\nWelches Land möchtest du buchen? (1-3): ");

            while (!int.TryParse(Console.ReadLine(), out auswahl)
                   || auswahl < 1
                   || auswahl > 3)
            {
                Console.Write("Bitte gib eine gültige Zahl zwischen 1 und 3 ein: ");
            }

            Reise gewaehlteReise = reisen[auswahl - 1];
            Console.Clear();

            Console.Write("Von wann reist du? (dd.MM.yyyy): ");
            DateTime vonDatum = DateTime.Parse(Console.ReadLine());

            Console.Write("Bis wann reist du? (dd.MM.yyyy): ");
            DateTime bisDatum = DateTime.Parse(Console.ReadLine());

            int personenAnzahl;

            Console.Write("Wie viele Personen reisen mit? (max. 5): ");

            while (!int.TryParse(Console.ReadLine(), out personenAnzahl)
                   || personenAnzahl <= 0
                   || personenAnzahl > 5)
            {
                Console.Write("Bitte gib eine Anzahl zwischen 1 und 5 ein: ");
            }

            Console.Clear();

            double gesamtPreis = 0;

            List<string> personenInfos = new List<string>();

            string[] sitzplaetze =
            {
                "A1", "A2", "A3",
                "B1", "B2", "B3",
                "C1", "C2", "C3",
                "D1"
            };

            List<string> belegtePlaetze = new List<string>();

            for (int i = 1; i <= personenAnzahl; i++)
            {
                Console.WriteLine($"\n===== PERSON {i} =====");

                Console.Write("Name: ");
                string name = Console.ReadLine();

                int alter;

                Console.Write("Alter: ");

                while (!int.TryParse(Console.ReadLine(), out alter))
                {
                    Console.Write("Bitte eine gültige Zahl eingeben: ");
                }

                double preis = gewaehlteReise.PreisProPerson;

                string rabattInfo = "Kein Rabatt";

                if (alter < 18)
                {
                    preis *= 0.9;
                    rabattInfo = "10% Rabatt";
                }

                Console.Write("Handgepäck dabei? (ja/nein): ");
                string handgepaeck = Console.ReadLine().ToLower();

                string kofferInfo = "Kein Koffer";

                Console.Write("Großen Koffer hinzufügen? (+30 Euro) (ja/nein): ");
                string koffer = Console.ReadLine().ToLower();

                if (koffer == "ja")
                {
                    preis += 30;
                    kofferInfo = "1 großer Koffer";
                }

                List<string> freiePlaetze = new List<string>();

                foreach (string platz in sitzplaetze)
                {
                    if (!belegtePlaetze.Contains(platz))
                    {
                        freiePlaetze.Add(platz);
                    }
                }

                Console.WriteLine("\nFreie Sitzplätze:");

                for (int j = 0; j < freiePlaetze.Count; j++)
                {
                    Console.WriteLine($"{j + 1}. {freiePlaetze[j]}");
                }

                int sitzAuswahl;

                Console.Write("\nWelchen Sitzplatz möchtest du wählen?: ");

                while (!int.TryParse(Console.ReadLine(), out sitzAuswahl)
                       || sitzAuswahl < 1
                       || sitzAuswahl > freiePlaetze.Count)
                {
                    Console.Write("Bitte eine gültige Nummer eingeben: ");
                }

                string gewaehlterSitz = freiePlaetze[sitzAuswahl - 1];

                belegtePlaetze.Add(gewaehlterSitz);

                gesamtPreis += preis;

                personenInfos.Add(
                    $"Name: {name} | Alter: {alter} | Sitzplatz: {gewaehlterSitz} | Gepäck: {kofferInfo} | Preis: {preis} Euro | {rabattInfo}"
                );

                Console.Clear();
            }

            Console.Clear();

            Console.WriteLine("=================================================");
            Console.WriteLine("                REISEÜBERSICHT");
            Console.WriteLine("=================================================");

            Console.WriteLine($"Reiseziel     : {gewaehlteReise.Land}");
            Console.WriteLine($"Zeitraum      : {vonDatum:dd.MM.yyyy} bis {bisDatum:dd.MM.yyyy}");
            Console.WriteLine($"Personenzahl  : {personenAnzahl}");

            Console.WriteLine("\n=================================================");
            Console.WriteLine("                 WETTERBERICHT");
            Console.WriteLine("=================================================");

            string[] wetterArten =
            {
    "Sonnig",
    "Leicht bewölkt",
    "Regnerisch",
    "Warm",
    "Windig"
};

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

            Console.WriteLine("\n=================================================");
            Console.WriteLine("                  RECHNUNG");
            Console.WriteLine("=================================================");

            Console.WriteLine("\n---------------- PERSONEN ----------------");

            foreach (string info in personenInfos)
            {
                Console.WriteLine(info);
            }

            Console.WriteLine("\n=================================================");
            Console.WriteLine($"GESAMTPREIS: {gesamtPreis} Euro");
            Console.WriteLine("=================================================");


            Console.WriteLine("\nVielen Dank für deine Buchung!");
            Console.WriteLine("Wir wünschen eine schöne Reise!");

            Console.ReadKey();
        }
    }
}