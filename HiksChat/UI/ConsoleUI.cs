using HiksChat.ChatManagement;
using HiksChat.Database;
using System;

namespace HiksChat.UI
{
    public class ConsoleUI
    {
        private readonly ConsoleColor defaultColor;
        private readonly User currentUser;
        private static int selectedOptionIndex = 0;
        private static DatabaseManager databaseManager = new DatabaseManager();

        public ConsoleUI(User user)
        {
            defaultColor = Console.ForegroundColor;
            currentUser = user;
            selectedOptionIndex = 0;
        }

        public static void RunMainMenu()
        {
            Console.Clear();
            Console.WriteLine("  _   _             _      _   _                ");
            Console.WriteLine(" | | | |           | |    | | (_)               ");
            Console.WriteLine(" | |_| | ___  _ __ | | ___| |_ _ _ __   __ _   ");
            Console.WriteLine(" |  _  |/ _ \\| '_ \\| |/ _ \\ __| | '_ \\ / _` |  ");
            Console.WriteLine(" | | | | (_) | |_) | |  __/ |_| | | | | (_| |  ");
            Console.WriteLine(" \\_| |_/\\___/| .__/|_|\\___|\\__|_|_| |_|\\__, |  ");
            Console.WriteLine("             | |                         __/ |  ");
            Console.WriteLine("             |_|                        |___/   ");
            Console.WriteLine();
            Console.WriteLine("Bitte wählen Sie eine Option:");
            string[] menuOptions = { "Anmelden", "Registrieren", "Programm beenden" };
            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedOptionIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }
                Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                Console.ResetColor();
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedOptionIndex > 0)
                    {
                        selectedOptionIndex--;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (selectedOptionIndex < menuOptions.Length - 1)
                    {
                        selectedOptionIndex++;
                    }
                    break;
                case ConsoleKey.Enter:
                    PerformSelectedAction(selectedOptionIndex);
                    break;
            }

            RunMainMenu(); // Nach der Auswahl zurück zum Hauptmenü gehen
        }

        private static void PerformSelectedAction(int selectedOptionIndex)
        {
            switch (selectedOptionIndex)
            {
                case 0:
                    LoginUser();
                    break;
                case 1:
                    RegisterUser();
                    break;
                case 2:
                    Environment.Exit(0);
                    break;
            }
        }

        private static void RegisterUser()
        {
            Console.WriteLine("Benutzerregistrierung:");
            Console.Write("Benutzername: ");
            string username = Console.ReadLine();
            Console.Write("Passwort: ");
            string password = Console.ReadLine();
            Console.Write("Sprache: ");
            string language = Console.ReadLine();

            // Hier wird der Benutzer in der Datenbank registriert
            databaseManager.SaveUser(username, password, language);

            Console.WriteLine("Benutzer erfolgreich registriert!");
            Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }

        private static void LoginUser()
        {
            Console.WriteLine("Anmelden:");
            Console.Write("Benutzername: ");
            string username = Console.ReadLine();
            Console.Write("Passwort: ");
            string password = Console.ReadLine();

            // Hier wird die Anmeldelogik mit der Datenbank überprüft
            bool loggedIn = databaseManager.CheckLogin(username, password);

            if (loggedIn)
            {
                Console.WriteLine($"Willkommen zurück, {username}!");
            }
            else
            {
                Console.WriteLine("Fehler bei der Anmeldung. Benutzername oder Passwort ungültig.");
            }

            Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }

        public void DisplayMessage(string sender, string message, ConsoleColor color, string emoji)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{sender} - {DateTime.Now:g}");
            Console.WriteLine($"{message}\n");
            Console.ResetColor();
        }

        public string GetUserInput(string prompt)
        {
            Console.Write($"{currentUser.Username}, {prompt}: ");
            return Console.ReadLine();
        }
    }
}
