using System;
using System.Net.Sockets;
using ServerClientLibrary;
using ServerClientLibrary.Packets;

namespace Server.TcpWrapper
{
    public class Tcp : ClientBase.TcpBase
    {
        public readonly Client Client;
        public readonly int Id;

        internal Tcp(Client client, TcpClient tcpClient) : base(tcpClient)
        {
            Client = client;
            Id = client.Id;
        }

        protected override void HandleReceivedData(byte[] data)
        {
            var packet = new Packet(data);

            var type = (PacketType)packet.ReadInt(false);

            IMessage message = type switch
            {
                PacketType.Welcome => new WelcomeMessage(),
                PacketType.UserMessage => new UserMessage(),
                PacketType.ServerMessage => throw new Exception("Client can't send server packets!"),
                _ => throw new ArgumentOutOfRangeException()
            };

            message.FromPacket(packet);

            switch (message)
            {
                case WelcomeMessage welcome:
                    Client.Name = welcome.UserName;
                    break;
                case UserMessage userMessage:
                    Client.MessageReceive(Id, userMessage);
                    break;
            }

        }
    }
}
