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

        }
    }
}