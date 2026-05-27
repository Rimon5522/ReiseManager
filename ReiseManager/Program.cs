using System;
using System.Collections.Generic;
using System.IO;

namespace ReiseProgramm
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            // LOGIN SYSTEM

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("======================================");
            Console.WriteLine("     REISEBUERO ACCOUNT SYSTEM");
            Console.WriteLine("======================================");

            Console.ResetColor();

            Console.Write("Erstelle einen Benutzernamen: ");
            string benutzername = Console.ReadLine();

            Console.Write("Erstelle ein Passwort: ");
            string passwort = Console.ReadLine();

            Console.Clear();

            int versuche = 3;
            bool zugang = false;

            while (versuche > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("========== LOGIN ==========");
                Console.ResetColor();

                Console.Write("Benutzername: ");
                string loginName = Console.ReadLine();

                Console.Write("Passwort: ");
                string loginPasswort = Console.ReadLine();

                if (loginName == benutzername && loginPasswort == passwort)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nZugang gewährt!");
                    Console.ResetColor();

                    Console.Beep(1000, 300);

                    zugang = true;
                    break;
                }
                else
                {
                    versuche--;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLogin fehlgeschlagen!");
                    Console.ResetColor();

                    Console.Beep(300, 700);

                    if (versuche > 0)
                    {
                        Console.WriteLine($"Noch {versuche} Versuche.\n");
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

            Console.Clear();

            // BENUTZERDATEN

            Benutzer benutzer = new Benutzer();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========== DEINE DATEN ==========");
            Console.ResetColor();

            Console.Write("Vorname: ");
            benutzer.Vorname = Console.ReadLine();

            Console.Write("Nachname: ");
            benutzer.Nachname = Console.ReadLine();

            Console.Write("Alter: ");
            benutzer.Alter = Console.ReadLine();

            Console.Write("E-Mail: ");
            benutzer.Email = Console.ReadLine();

            // REISEN

            List<Reise> reisen = new List<Reise>()
            {
                new Reise("Spanien", 600),
                new Reise("Italien", 750),
                new Reise("Japan", 1000)
            };

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========== REISEBUCHUNG ==========");
            Console.ResetColor();

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
                Console.Write("Bitte gültig eingeben: ");
            }

            Reise gewaehlteReise = reisen[auswahl - 1];

            Console.Clear();

            // FLUGINFO

            FlugInfo flugInfo = new FlugInfo();

            Console.Write("Von wann reist du? (dd.MM.yyyy): ");
            flugInfo.VonDatum = DateTime.Parse(Console.ReadLine());

            Console.Write("Bis wann reist du? (dd.MM.yyyy): ");
            flugInfo.BisDatum = DateTime.Parse(Console.ReadLine());

            flugInfo.Flugnummer = "FL" + random.Next(1000, 9999);

            string[] gates = { "A1", "B4", "C2", "D8" };
            flugInfo.Gate = gates[random.Next(gates.Length)];

            // PERSONEN

            int personenAnzahl;

            Console.Write("Wie viele Personen reisen mit? (max. 5): ");

            while (!int.TryParse(Console.ReadLine(), out personenAnzahl)
                   || personenAnzahl <= 0
                   || personenAnzahl > 5)
            {
                Console.Write("Bitte 1-5 eingeben: ");
            }

            Console.Clear();

            SitzplatzSystem sitzplatzSystem = new SitzplatzSystem();
            Rechnung rechnung = new Rechnung();

            for (int i = 1; i <= personenAnzahl; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n===== PERSON {i} =====");
                Console.ResetColor();

                Console.Write("Name: ");
                string name = Console.ReadLine();

                int alter;

                Console.Write("Alter: ");

                while (!int.TryParse(Console.ReadLine(), out alter))
                {
                    Console.Write("Bitte gültige Zahl eingeben: ");
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

                string handgepaeckInfo = handgepaeck == "ja" ? "Ja" : "Nein";

                Console.Write("Großen Koffer hinzufügen? (+30 Euro) (ja/nein): ");
                string koffer = Console.ReadLine().ToLower();

                string kofferInfo = "Kein Koffer";

                if (koffer == "ja")
                {
                    preis += 30;
                    kofferInfo = "Großer Koffer";
                }

                sitzplatzSystem.FreiePlaetzeAnzeigen();

                int sitzAuswahl;

                Console.Write("\nSitzplatz wählen: ");

                while (!int.TryParse(Console.ReadLine(), out sitzAuswahl)
                       || sitzAuswahl < 1
                       || sitzAuswahl > sitzplatzSystem.FreiePlaetze.Count)
                {
                    Console.Write("Bitte gültig eingeben: ");
                }

                string gewaehlterSitz = sitzplatzSystem.FreiePlaetze[sitzAuswahl - 1];

                sitzplatzSystem.BelegtePlaetze.Add(gewaehlterSitz);

                rechnung.GesamtPreis += preis;

                rechnung.PersonenInfos.Add(
                    $"{name,-15} | {alter,-3} Jahre | Sitz: {gewaehlterSitz,-3} | Handgepäck: {handgepaeckInfo,-3} | Gepäck: {kofferInfo,-15} | {preis} Euro | {rabattInfo}"
                );

                Console.Clear();
            }

            // REISEÜBERSICHT

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("=================================================");
            Console.WriteLine("                REISEÜBERSICHT");
            Console.WriteLine("=================================================");

            Console.ResetColor();

            Console.WriteLine($"Reiseziel     : {gewaehlteReise.Land}");
            Console.WriteLine($"Zeitraum      : {flugInfo.VonDatum:dd.MM.yyyy} bis {flugInfo.BisDatum:dd.MM.yyyy}");
            Console.WriteLine($"Personenzahl  : {personenAnzahl}");
            Console.WriteLine($"Flugnummer    : {flugInfo.Flugnummer}");
            Console.WriteLine($"Gate          : {flugInfo.Gate}");

            // WETTERBERICHT

            Wetterbericht wetterbericht = new Wetterbericht();

            wetterbericht.WetterAnzeigen(random, flugInfo.VonDatum, flugInfo.BisDatum);

            // RECHNUNG

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("\n=================================================");
            Console.WriteLine("                    RECHNUNG");
            Console.WriteLine("=================================================");

            Console.ResetColor();

            foreach (string info in rechnung.PersonenInfos)
            {
                Console.WriteLine(info);
            }

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\n=================================================");
            Console.WriteLine($"GESAMTPREIS: {rechnung.GesamtPreis} Euro");
            Console.WriteLine("=================================================");

            Console.ResetColor();

            // FLUGTICKET

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.WriteLine("\n================ FLUGTICKET =================");

            Console.ResetColor();

            Console.WriteLine($"BENUTZER     : {benutzer.Vorname} {benutzer.Nachname}");
            Console.WriteLine($"REISEZIEL    : {gewaehlteReise.Land}");
            Console.WriteLine($"FLUGNUMMER   : {flugInfo.Flugnummer}");
            Console.WriteLine($"GATE         : {flugInfo.Gate}");
            Console.WriteLine($"ABFLUG       : {flugInfo.VonDatum:dd.MM.yyyy}");

            // DATEI SPEICHERN

            string rechnungText =
$@"REISEBUCHUNG

Benutzer: {benutzer.Vorname} {benutzer.Nachname}
Reiseziel: {gewaehlteReise.Land}
Zeitraum: {flugInfo.VonDatum:dd.MM.yyyy} bis {flugInfo.BisDatum:dd.MM.yyyy}
Flugnummer: {flugInfo.Flugnummer}
Gate: {flugInfo.Gate}

GESAMTPREIS: {rechnung.GesamtPreis} Euro";

            File.WriteAllText("Rechnung.txt", rechnungText);

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\nRechnung wurde als Datei gespeichert!");

            Console.ResetColor();

            Console.WriteLine("\nVielen Dank für deine Buchung!");
            Console.WriteLine("Wir wünschen eine schöne Reise!");

            Console.ReadKey();
        }
    }
}