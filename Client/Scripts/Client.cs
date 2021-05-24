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

        public static  bool ServerAvailable(string ip,int port)
        {
            using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Connect(ip, port);
            }
            catch (SocketException)
            {
                return false;
            }
            finally
            {
                socket.Dispose();
            }

            return true;

            //if (string.IsNullOrEmpty(ip) || string.IsNullOrWhiteSpace(ip) || IPAddress.TryParse(ip, out _) == false)
            //    return IPStatus.BadDestination;

            //var ping = new Ping();
            //var reply = await ping.SendPingAsync(ip);

            //return reply?.Status ?? IPStatus.BadDestination;
        }
    }
}