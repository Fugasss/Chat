using System;
using System.IO;
using System.Net.Sockets;
using ServerClientLibrary;
using ServerClientLibrary.Packets;

namespace Server.TcpWrapper
{
    public class Client : ClientBase
    {
        public static event Action<IMessage, int> OnMessageReceive = delegate { };

        public int Id { get; private set; }
        public string Name { get; internal set; } = "UserName";
        public WelcomeMessage WelcomeMessage { get; internal set; } = WelcomeMessage.CreateInstance();

        public Client(int id, TcpClient tcp)
        {
            Id = id;
            Tcp = new Tcp(id, tcp);
        }

        public override string ToString() => Name;

        internal static void MessageReceive(int id, IMessage message) => OnMessageReceive?.Invoke(message, id);

        public static void SetName(int id, WelcomeMessage welcomeMessage)
        {
            if (!Server.Clients.TryGetValue(id, out var client)) return;

            client.Name = welcomeMessage.UserName;
            client.WelcomeMessage = welcomeMessage;
        }
    }
}