namespace ServerClientLibrary.Packets
{
    public struct WelcomeMessage : IMessage
    {
        public PacketType Type { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }

        public Packet GetPacket()
        {
            var packet = new Packet((int)Type);
            packet.Write(UserName);
            packet.Write(Message);

            return packet;
        }

        public void FromPacket(Packet packet)
        {
            Type = (PacketType)packet.ReadInt();
            UserName = packet.ReadString();
            Message = packet.ReadString();
        }

        public void FromPacket(byte[] data)
        {
            FromPacket(new Packet(data));
        }

    }
}