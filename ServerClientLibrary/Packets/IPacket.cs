namespace ServerClientLibrary.Packets
{
    public interface IPacket
    {
        public PacketType Type { get; set; }

        public Packet GetPacket();
        public void FromPacket(Packet packet);
        public void FromPacket(byte[] data);
    }
}
