using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace HiksServer.WebSocketServerApp
{
    public class WebSocketServer : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            string message = e.Data;
            Console.WriteLine($"Received message: {message}");
            Send(message);
        }
    }
}
