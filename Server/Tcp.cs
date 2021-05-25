using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;
using ServerClientLibrary;
using ServerClientLibrary.Packets;

namespace Server.TcpWrapper
{
    public class Tcp : ClientBase.TcpBase
    {
        public readonly int Id;

        internal Tcp(int id, TcpClient tcpClient) : base(tcpClient)
        {
            Id = id;
        }

        protected override void HandleReceivedData(byte[] data)
        {
            var packet = new Packet(data);

            var type = (PacketType)packet.ReadInt(false);

            IMessage message = type switch
            {
                PacketType.Welcome => WelcomeMessage.CreateInstance(),
                PacketType.UserMessage => UserMessage.CreateInstance(),
                PacketType.ServerMessage => throw new Exception("Client can't send server packets!"),
                _ => throw new ArgumentOutOfRangeException()
            };

            message.FromPacket(packet);

            switch (message)
            {
                case WelcomeMessage welcome:
                    Client.SetName(Id, welcome);
                    Client.MessageReceive(Id, welcome);
                    break;
                case UserMessage userMessage:
                    Client.MessageReceive(Id, userMessage);
                    break;
            }

        }
    }
}
