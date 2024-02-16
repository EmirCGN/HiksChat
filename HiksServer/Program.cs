using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace HiksServer
{
    class Program
    {
        static void Main(string[] args)
        {
            string serverUrl = "ws://localhost:8080";
            var server = new WebSocketServer(serverUrl);
            server.AddWebSocketService<WebSocketServerApp.WebSocketServer>("/");
            server.Start();

            Console.WriteLine($"WebSocket server started at {serverUrl}");
            Console.ReadLine();

            server.Stop();
        }
    }
}
