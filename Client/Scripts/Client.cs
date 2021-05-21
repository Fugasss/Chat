using System;
using ServerClientLibrary;
using ServerClientLibrary.Packets;

namespace Chat.Scripts
{
    public class Client : ClientBase
    {
        public static event Action<IMessage> OnMessageReceive = delegate { };

        public readonly string Ip;
        public readonly int Port;

        public Client(string ip, int port)
        {
            Ip = ip;
            Port = port;

            Tcp = new Tcp(Ip, Port);
        }

        public void SendMessage(UserMessage message)
        {
            Tcp.SendPacket(message);
        }

        public static void MessageReceived(IMessage message) => OnMessageReceive?.Invoke(message);
    }
}