namespace ServerClientLibrary.Packets
{
    public struct UserMessage : IMessage
    {
        public static UserMessage CreateInstance()
        {
            var newMessage = new UserMessage();

            newMessage.Type = PacketType.UserMessage;
            newMessage.Message = string.Empty;
            newMessage.Name = string.Empty;

            return newMessage;
        }

        public PacketType Type { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }

        public Packet GetPacket()
        {
            var packet = new Packet();
            packet.Write((int) Type);
            packet.Write(Name);
            packet.Write(Message);

            return packet;
        }

        public void FromPacket(Packet packet)
        {
            Type = (PacketType)packet.ReadInt();
            Name = packet.ReadString();
            Message = packet.ReadString();
        }

        public void FromPacket(byte[] data)
        {
            FromPacket(new Packet(data));
        }
    }
}