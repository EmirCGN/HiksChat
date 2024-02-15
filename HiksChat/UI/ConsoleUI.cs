using HiksChat.ChatManagement;
using HiksChat.Database;
using Microsoft.IdentityModel.Tokens;
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
            try
            {
                Console.Clear();
                Console.CursorVisible = false;

                PrintLogo();
                PrintMenuOptions();

                HandleUserInput();

                RunMainMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void PrintLogo()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
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
            Console.ResetColor();
        }

        private static void PrintMenuOptions()
        {
            Console.WriteLine("\t\t\t\t\t\t Please select an option:");
            string[] menuOptions = { "Login", "Register", "Exit" };
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

        private static string ReadPassword() 
        {
            string password = "";
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, (password.Length - 1));
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return password;
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
            try
            {
                Console.WriteLine("User Registration:");
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                Console.Write("Language: ");
                string language = Console.ReadLine();

                databaseManager.SaveUser(username, password, language);

                Console.WriteLine($"User successfully registered! {username}");
                Thread.Sleep(2000);

                ConsoleUI chatUI = new ConsoleUI(new User { Username = username });
                chatUI.RunChatMenu();

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during registration: {ex.Message}");
            }
        }

        private static void LoginUser()
        {
            try
            {
                Console.WriteLine("Login:");
                Console.Write("Username: ");
                string username = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(username))
                {
                    Console.WriteLine("Username cannot be empty. Please try again.");
                    return;
                }
                Console.Write("Password: ");
                string password = ReadPassword();
                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("Password cannot be empty. Please try again.");
                    return;
                }

                bool loggedIn = databaseManager.CheckLogin(username, password);

                if (loggedIn)
                {
                    Console.WriteLine($"Welcome back, {username}!");
                    Thread.Sleep(3000);
                    ConsoleUI chatUI = new ConsoleUI(new User { Username = username });
                    chatUI.RunChatMenu();
                }
                else
                {
                    Console.WriteLine("Login failed. Invalid username or password.");
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during login: {ex.Message}");
            }
        }

        #endregion

        #region Chat

        public void RunChatMenu()
        {
            try
            {
                Console.Clear();
                Console.Title = $"HiksChat - {currentUser.Username}";
                Console.WriteLine($"Welcome to the Chat Menu, {currentUser.Username}!");

                while (true)
                {
                    PrintChatMenuOptions();
                    HandleChatMenuInput();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred in the chat menu: {ex.Message}");
            }
        }

        private void PrintChatMenuOptions()
        {
            Console.WriteLine("\nPlease select an option:");
            string[] menuOptions = { "Chat with other users", "Create group", "Settings", "Logout" };
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
                    // Implement chat with other users
                    break;
                case 1:
                    // Implement create group
                    break;
                case 2:
                    // Implement settings
                    break;
                case 3:
                    Console.WriteLine($"You have been logged out, {currentUser.Username}.");
                    Console.ReadLine();
                    return;
            }
        }

        #endregion

        #region Display
        public void DisplayMessage(string sender, string message, ConsoleColor color, string emoji)
        {
            Console.ForegroundColor = color;
            Console.WriteLine($"{currentUser.Username} - {DateTime.Now:g}");
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
