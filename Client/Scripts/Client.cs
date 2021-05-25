using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using ServerClientLibrary;
using ServerClientLibrary.Packets;

namespace Chat.Scripts
{
    public class Client : ClientBase
    {
        public static event Action<IMessage> OnMessageReceive = delegate { };

        public readonly string Ip;
        public readonly int Port;
        public readonly string UserName;

        public Client(string ip, int port, string username)
        {
            Ip = ip;
            Port = port;
            UserName = username;

            Tcp = new Tcp(Ip, Port);

            var welcome = WelcomeMessage.CreateInstance();

            welcome.UserName = username;
            welcome.Message = string.Empty;

            Tcp.SendPacket(welcome);
        }

        public void SendMessage(UserMessage message)
        {
            Tcp.SendPacket(message);
        }

        public static void MessageReceived(IMessage message) => OnMessageReceive?.Invoke(message);

        public static bool ServerAvailable(string ip, int port)
        {
            if (string.IsNullOrEmpty(ip) || string.IsNullOrWhiteSpace(ip) || IPAddress.TryParse(ip, out var iPAddress) == false)
                return false;

            const int timeout = 150;

            var ping = new Ping();
            var reply = ping.Send(iPAddress, timeout);

            return reply != null && reply.Status == IPStatus.Success;
        }
    }
}