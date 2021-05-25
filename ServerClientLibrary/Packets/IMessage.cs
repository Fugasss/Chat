namespace ServerClientLibrary.Packets
{
    public interface IMessage : IPacket
    {
        public string Message { get; }

    }
}