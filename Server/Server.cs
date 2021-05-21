using ServerClientLibrary;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Server.TcpWrapper;
using ServerClientLibrary.Packets;

namespace Server
{
    class Server
    {
        private readonly Dictionary<int, Client> m_Clients;
        private readonly TcpListener m_Listener;
        private int m_LastId = 0;

        public int Port { get; }

        public Server(int port)
        {
            Port = port;

            m_Listener = new TcpListener(IPAddress.Any, Port);

            m_Clients = new Dictionary<int, Client>();
        }

        public void Start()
        {
            try
            {
                m_Listener.Start();

                Log("Server started");

                Client.OnMessageReceive += Log;

                m_Listener.BeginAcceptTcpClient(AcceptClientAsync, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void Log(string message) =>
            Console.WriteLine(message);
        private void Log(UserMessage message, int clientId)
        {
            m_Clients.TryGetValue(clientId, out var clientName);

            Console.WriteLine("{0} {1}", message.Name, message.Message);
            if (clientId == -1) return;

            var name = "User";
            if (m_Clients.TryGetValue(clientId, out var currentClient))
                name = currentClient.Name;

            message.Name = name;

            for (var i = 0; i < m_Clients.Count; i++)
            {
                if (!m_Clients.TryGetValue(i, out var client)) continue;
                if (client.Id == clientId) continue;

                client.Tcp.SendPacket(message);
            }
        }

        private void AcceptClientAsync(IAsyncResult ar)
        {
            try
            {
                Log("Client connected");
                var tcp = m_Listener.EndAcceptTcpClient(ar);

                var client = new Client(m_LastId, tcp);

                m_Clients.Add(m_LastId, client);
                m_LastId++;

                m_Listener.BeginAcceptTcpClient(AcceptClientAsync, null);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}