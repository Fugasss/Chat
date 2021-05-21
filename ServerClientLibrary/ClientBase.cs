using System;
using System.Net.Sockets;
using ServerClientLibrary.Packets;

namespace ServerClientLibrary
{
    public abstract class ClientBase
    {
        public TcpBase Tcp { get; protected set; }

        public abstract class TcpBase
        {
            public TcpClient Socket { get; private set; }
            public NetworkStream Stream { get; private set; }
            protected byte[] m_Buffer = new byte[Constants.BUFFER_SIZE];

            protected TcpBase(TcpClient client)
            {
                Socket = client;
                Stream = Socket.GetStream();

                Stream.BeginRead(m_Buffer, 0, Constants.BUFFER_SIZE, ReceiveDataAsync, null);
            }
            protected TcpBase(string ip, int port) : this(new TcpClient(ip, port)) { }

            ~TcpBase()
            {
                Stream.Close();
                Socket.Close();
                Stream = null;
                Socket = null;
                m_Buffer = null;
            }

            public void SendPacket(byte[] data)
            {
                Array.Copy(data, m_Buffer, data.Length);
                Stream.BeginWrite(m_Buffer, 0, Constants.BUFFER_SIZE, SendDataAsync, null);
            }
            public void SendPacket(IPacket data) =>
                SendPacket(data.GetPacket().ToArray());

            private void ReceiveDataAsync(IAsyncResult ar)
            {
                try
                {
                    var length = Stream.EndRead(ar);

                    if (length <= 0)
                        return;

                    var data = new byte[Constants.BUFFER_SIZE];
                    Array.Copy(m_Buffer, data, length);

                    HandleReceivedData(data);

                    Stream.BeginRead(m_Buffer, 0, Constants.BUFFER_SIZE, ReceiveDataAsync, null);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }
            }
            private void SendDataAsync(IAsyncResult ar)
            {
                try
                {
                    Stream.EndWrite(ar);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            protected abstract void HandleReceivedData(byte[] data);
        }
    }
}