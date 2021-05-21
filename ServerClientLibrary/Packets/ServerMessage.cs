namespace ServerClientLibrary.Packets
{
    public struct ServerMessage : IMessage
    {
        public string Message { get; set; }

        public PacketType Type { get; set; }

        public Packet GetPacket()
        {
            var packet = new Packet((int)Type);
            packet.Write(Message);

            return packet;
        }

        public void FromPacket(Packet packet)
        {
            Type = (PacketType)packet.ReadInt();
            Message = packet.ReadString();
        }

        public void FromPacket(byte[] data) => FromPacket(new Packet(data));
    }
}