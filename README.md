# HiksChat

HiksChat is a chat application that was developed in C# and offers various functions for user and group management as well as for communication. Here is an overview of the most important classes and their functions:

## UML diagram

```mermaid
classDiagram
    User --|> List
    User --o ChatGroup
    class User {
        - int UserId
        - string Username
        - string Password
        - string Language
        - string PreferredLanguage
        - List&lt;ChatGroup&gt; Groups
        + void RegisterUser(string username, string password, string language)
        + List&lt;ChatGroup&gt; GetGroups()
    }

    TranslationService --|> List
    class TranslationService {
        - int TranslationId
        - string ApiKey
        - List&lt;string&gt; supportedLanguages
        + string Translate(string text, string targetLanguage)
        + string DetectLanguage(string text)
        + List&lt;string&gt; GetSupportedLanguages()
        + void AddSupportedLanguage(string language)
        + void RemoveSupportedLanguage(string language)
    }

    ChatServer --|> List
    ChatServer --o ChatGroup
    ChatServer --o ChatClient
    class ChatServer {
        - List&lt;ChatGroup&gt; Groups
        - List&lt;ChatClient&gt; Users
        + ChatGroup CreateGroup(string groupName)
        + void JoinGroup(ChatClient client, string groupName)
        + ChatGroup GetGroupByName(string groupName)
        + ChatGroup GetGroupById(int groupId)
        + void RemoveGroup(int groupId)
        + ChatClient GetUserById(int userId)
        + ChatClient GetUserByUsername(string username)
        + void RemoveUser(int userId)
        + void ChangeUsername(int userId, string newUsername)
        + void ChangeLanguage(int userId, string newLanguage)
        + void CreateChat(ChatClient user1, ChatClient user2)
    }

    ChatGroup --|> List
    ChatGroup --o ChatClient
    class ChatGroup {
        - int GroupId
        - List&lt;ChatClient&gt; Members
        - string Name
        + void AddMember(ChatClient member)
        + void RemoveMember(ChatClient member)
        + void SaveToDatabase()
        + static ChatGroup LoadFromDatabase(int groupId)
        + void SaveMessageToDatabase(string sender, string content)
        + List&lt;string&gt; GetChatHistoryFromDatabase()
        + int GetGroupId()
    }

    ChatClient --|> List
    ChatClient --o User
    ChatClient --o TranslationService
    class ChatClient {
        - User User
        - TranslationService translationService
        - List&lt;string&gt; messageHistory
        - string preferredLanguage
        + void SetPreferredLanguage(string language)
        + void SendMessage(string message)
        + string ReceiveMessage()
        + List&lt;string&gt; ViewChatHistory()
        + void ClearChatHistory()
        + void ProcessCommand(string command)
        + int CalculatePing()
    }

    Program --|> DatabaseManager
    Program --o ConsoleUI
    class Program {
        + void Main(string[] args)
    }

    ConsoleUI --|> User
    ConsoleUI --|> DatabaseManager
    class ConsoleUI {
        - ConsoleColor defaultColor
        - User currentUser
        + static int selectedOptionIndex
        + static DatabaseManager databaseManager
        + ConsoleUI(User user)
        + static void RunMainMenu()
        + void RunChatMenu()
        + void DisplayMessage(string sender, string message, ConsoleColor color, string emoji)
        + string GetUserInput(string prompt)
    }

    DatabaseManager --|> List
    class DatabaseManager {
        - static string connectionString
        + void InitializeDatabase()
        + void SaveUser(string username, string password, string language)
        + bool CheckLogin(string username, string password)
    }

    DatabaseManager --|> List
    class DatabaseManager {
        - static string connectionString
        + void InitializeDatabase()
        + void SaveUser(string username, string password, string language)
        + bool CheckLogin(string username, string password)
    }
```
These classes work together to create a fully functional chat application that allows users to communicate with each other and organize group chats.
