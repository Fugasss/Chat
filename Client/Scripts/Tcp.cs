using System;
using ServerClientLibrary;
using ServerClientLibrary.Packets;

namespace Chat.Scripts
{
    public class Tcp : ClientBase.TcpBase
    {
        public Tcp(string ip, int port) : base(ip, port) { }

        protected override void HandleReceivedData(byte[] data)
        {
            var packet = new Packet(data);
            var type = (PacketType)packet.ReadInt(false);

            IMessage message = type switch
            {
                PacketType.Welcome => new WelcomeMessage(),
                PacketType.UserMessage => new UserMessage(),
                PacketType.ServerMessage => new ServerMessage(),
                _ => throw new ArgumentOutOfRangeException()
            };

            message.FromPacket(packet);

            Client.MessageReceived(message);
        }
    }
}