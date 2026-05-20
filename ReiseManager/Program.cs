using System;
using System.Collections.Generic;

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
            var password = "5522";
            int versuche = 3;

            Console.WriteLine("Bitte gib dein Passwort ein: ");
            if (password == Console.ReadLine())
            {
                Console.WriteLine("Zugang gewährt");
            }
            else
            {
                versuche--;
                Console.WriteLine("Passwort falsch du hast noch " + versuche + " Versuche");
                Console.WriteLine("Bitte gib dein Passwort ein: ");
                if (password == Console.ReadLine())
                {
                    Console.WriteLine("Zugang gewährt");
                }
                else
                {
                    versuche--;
                    Console.WriteLine("Passwort falsch du hast noch " + versuche + " Versuch");
                    Console.WriteLine("Bitte gib dein Passwort ein: ");
                    if (password == Console.ReadLine())
                    {
                        Console.WriteLine("Zugang gewährt");
                    }
                    else
                    {
                        versuche--;
                        Console.WriteLine("Passwort falsch du hast noch " + versuche + " Versuche");
                        Console.WriteLine("Konto gespeert!");
                    }
                }
            }

            string firstName, lastName, age, email;

            Console.WriteLine("Hallo, bitte gib deinen Vornamen ein: ");
            firstName = Console.ReadLine() ?? "";

            Console.WriteLine("Gib als nächstes deinen Nachnamen ein: ");
            lastName = Console.ReadLine() ?? "";

            Console.WriteLine("Gib dein Alter ein: ");
            age = Console.ReadLine() ?? "";

            Console.WriteLine("Gib deine E-Mail ein: ");
            email = Console.ReadLine() ?? "";

            Console.WriteLine("\n\nVorname: " + firstName + "\nNachname: " + lastName + "\nAlter: " + age + "\nE-Mail: " + email);


            List<Reise> reisen = new List<Reise>()
            {
                new Reise("Spanien", 500),
                new Reise("Italien", 650),
                new Reise("Japan", 1200)
            };
            Console.WriteLine("======================\n");
            Console.WriteLine("==== REISEBUCHUNG ====\n");

            for (int i = 0; i < reisen.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {reisen[i].Land} - {reisen[i].PreisProPerson}Euro");
            }

            Console.Write("\nWelches Land möchtest du buchen? (1-3): ");
            int auswahl = Convert.ToInt32(Console.ReadLine());

            Reise gewaehlteReise = reisen[auswahl - 1];

            Console.Write("Von wann reist du? ");
            string vonDatum = Console.ReadLine();

            Console.Write("Bis wann reist du? ");
            string bisDatum = Console.ReadLine();

            Console.Write("Wie viele Personen reisen mit? ");
            int personenAnzahl = Convert.ToInt32(Console.ReadLine());

            double gesamtPreis = 0;

            List<string> personenInfos = new List<string>();

            for (int i = 1; i <= personenAnzahl; i++)
            {
                Console.Write($"Alter von Person {i}: ");
                int alter = Convert.ToInt32(Console.ReadLine());

                double preis = gewaehlteReise.PreisProPerson;

                string rabattInfo = "Kein Rabatt";

                if (alter < 18)
                {
                    preis = preis * 0.9; // 10% Rabatt
                    rabattInfo = "10% Rabatt (unter 18)";
                }

                gesamtPreis += preis;

                personenInfos.Add(
                    $"Person {i} | Alter: {alter} | Preis: {preis} Euro | {rabattInfo}"
                );
            }

            Console.WriteLine("\n============================");
            Console.WriteLine("         RECHNUNG");
            Console.WriteLine("============================");

            Console.WriteLine($"Reiseziel: {gewaehlteReise.Land}");
            Console.WriteLine($"Zeitraum: {vonDatum} bis {bisDatum}");
            Console.WriteLine($"Personenanzahl: {personenAnzahl}");

            Console.WriteLine("\n--- Personen ---");

            foreach (string info in personenInfos)
            {
                Console.WriteLine(info);
            }

            Console.WriteLine("\n============================");
            Console.WriteLine($"GESAMTPREIS: {gesamtPreis}Euro");
            Console.WriteLine("============================");

            Console.WriteLine("\nVielen Dank für deine Buchung!");
            Console.ReadKey();
            Console.WriteLine("\nWir wünschen eine schöne Reise!"); 
            Console.ReadKey();  
        }
    }
}