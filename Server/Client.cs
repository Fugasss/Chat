using System;
using System.IO;
using System.Net.Sockets;
using ServerClientLibrary;
using ServerClientLibrary.Packets;

namespace Server.TcpWrapper
{
    public class Client : ClientBase
    {
        public static event Action<UserMessage, int> OnMessageReceive = delegate { };

        public int Id { get; private set; }
        public string Name { get; internal set; } = "UserName";

        public Client(int id, TcpClient tcpClient)
        {
            Id = id;

            Tcp = new Tcp( this, tcpClient);
        }

        public override string ToString() => Name;

        internal static void MessageReceive(int id, UserMessage message) => OnMessageReceive?.Invoke(message, id);
    }
}