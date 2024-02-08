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

        #region Main Menu

        public static void RunMainMenu()
        {
            Console.Clear(); // Console löschen
            Console.CursorVisible = false; // Cursor unsichtbar machen

            PrintLogo();
            PrintMenuOptions();

            HandleUserInput();

            RunMainMenu(); // Nach der Auswahl zurück zum Hauptmenü gehen
        }

        private static void PrintLogo()
        {
            string[] logoLines = {
                @"  /$$   /$$ /$$ /$$                  /$$$$$$  /$$                   /$$    ",
                @" | $$  | $$|__/| $$                 /$$__  $$| $$                  | $$    ",
                @" | $$  | $$ /$$| $$   /$$  /$$$$$$$| $$  \__/| $$$$$$$   /$$$$$$  /$$$$$$  ",
                @" | $$$$$$$$| $$| $$  /$$/ /$$_____/| $$      | $$__  $$ |____  $$|_  $$_/  ",
                @" | $$__  $$| $$| $$$$$$/ |  $$$$$$ | $$      | $$  \ $$  /$$$$$$$  | $$    ",
                @" | $$  | $$| $$| $$_  $$  \____  $$| $$    $$| $$  | $$ /$$__  $$  | $$ /$$",
                @" | $$  | $$| $$| $$ \  $$ /$$$$$$$/|  $$$$$$/| $$  | $$|  $$$$$$$  |  $$$$/",
                @" |__/  |__/|__/|__/  \__/|_______/  \______/ |__/  |__/ \_______/   \___/  ",
                ""
            };

            int windowWidth = Console.WindowWidth;
            foreach (string line in logoLines)
            {
                Console.WriteLine(CenterText(line, windowWidth));
            }

            Console.WriteLine();
        }

        private static void PrintMenuOptions()
        {
            Console.WriteLine("\t\t\t\t\t\t Bitte wählen Sie eine Option:");
            string[] menuOptions = { "Anmelden", "Registrieren", "Programm beenden" };
            int longestOptionLength = menuOptions.Max(option => option.Length);
            int padding = (Console.WindowWidth - longestOptionLength) / 2;
            for (int i = 0; i < menuOptions.Length; i++)
            {
                if (i == selectedOptionIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(new string(' ', padding));
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(new string(' ', padding + 2));
                }
                Console.WriteLine($"{i + 1}. {menuOptions[i]}");
                Console.ResetColor();
            }
        }

        private static void HandleUserInput()
        {
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
                    if (selectedOptionIndex < 2)
                    {
                        selectedOptionIndex++;
                    }
                    break;
                case ConsoleKey.Enter:
                    PerformSelectedAction(selectedOptionIndex);
                    break;
            }
        }

        #endregion

        #region Utility Methods

        public static string CenterText(string text, int width)
        {
            return text.PadLeft((width + text.Length) / 2).PadRight(width);
        }

        #endregion


        #region Action Methods

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

            ConsoleUI chatUI = new ConsoleUI(new User { Username = username }); // Nach der Registrierung zum Chat-Menü weiterleiten
            chatUI.RunChatMenu();

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
                ConsoleUI chatUI = new ConsoleUI(new User { Username = username }); // Nach der Anmeldung zum Chat-Menü weiterleiten
                chatUI.RunChatMenu();
            }
            else
            {
                Console.WriteLine("Fehler bei der Anmeldung. Benutzername oder Passwort ungültig.");
            }

            Console.WriteLine("Drücken Sie eine beliebige Taste, um fortzufahren...");
            Console.ReadKey();
        }

        #endregion

        #region chat

        public void RunChatMenu()
        {
            Console.Clear();
            Console.WriteLine($"Willkommen im Chat-Menü, {currentUser.Username}!");

            while (true)
            {
                PrintChatMenuOptions();
                HandleChatMenuInput();
            }
        }

        private void PrintChatMenuOptions()
        {
            Console.WriteLine("\nBitte wählen Sie eine Option:");
            string[] menuOptions = { "Mit anderen Benutzern chatten", "Gruppe erstellen", "Einstellungen", "Abmelden" };
            Console.Clear();
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
        }

        private void HandleChatMenuInput()
        {
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
                    if (selectedOptionIndex < 3)
                    {
                        selectedOptionIndex++;
                    }
                    break;
                case ConsoleKey.Enter:
                    PerformChatMenuAction(selectedOptionIndex);
                    break;
            }
        }

        private void PerformChatMenuAction(int selectedOptionIndex)
        {
            switch (selectedOptionIndex)
            {
                case 0:
                    // Chat mit anderen Benutzern
                    break;
                case 1:
                    // Gruppe erstellen
                    break;
                case 2:
                    // Einstellungen
                    break;
                case 3:
                    // Abmelden
                    Console.WriteLine($"Sie wurden abgemeldet, {currentUser.Username}.");
                    Console.ReadLine();
                    return; // Zurück zum Hauptmenü
            }
        }

        #endregion

        #region display
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

        #endregion
    }
}
