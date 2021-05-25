using ServerClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Server.TcpWrapper;
using ServerClientLibrary.Packets;

namespace Server
{
    internal class Server
    {
        private readonly TcpListener m_Listener;
        private int m_LastId = 0;

        public static Dictionary<int, Client> Clients { get; private set; }
        public int Port { get; }

        public Server(int port)
        {
            Port = port;

            m_Listener = new TcpListener(IPAddress.Any, Port);

            Clients = new Dictionary<int, Client>();
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

        private static void Log(IMessage message, int clientId)
        {
            switch (message)
            {
                case UserMessage userMessage:
                    Log($"{userMessage.Name} {userMessage.Message}");
                    break;
                case WelcomeMessage welcomeMessage:
                    Log($"{welcomeMessage.UserName} connected: {welcomeMessage.Message}");
                    SendListOfUsers(clientId);
                    break;
            }

            if (clientId == -1 || Clients.Count <= 1) return;

            SendForAllUsers(message, clientId);
        }

        private static void SendForAllUsers(IPacket packet, int senderId)
        {
            for (var i = 0; i < Clients.Count; i++)
            {
                if (Clients.TryGetValue(i, out var client) == false) continue;
                if (client.Id == senderId) continue;
                if (client.Tcp.Socket.Connected == false) continue;

                client.Tcp.SendPacket(packet);
            }
        }

        private static void SendListOfUsers(int senderId)
        {
            if (!Clients.TryGetValue(senderId, out var client)) return;

            foreach (var user in Clients.Values.Where(user => user != null && user.Id != senderId))
            {
                var packet = user.WelcomeMessage;

                if (string.IsNullOrEmpty(packet.UserName))
                    packet.UserName = user.Name;

                client.Tcp.SendPacket(packet);
            }
        }

        private void AcceptClientAsync(IAsyncResult ar)
        {
            try
            {
                Log("Client connected");
                var tcp = m_Listener.EndAcceptTcpClient(ar);

                var client = new Client(m_LastId, tcp);

                Clients.Add(m_LastId, client);
                m_LastId++;

                m_Listener.BeginAcceptTcpClient(AcceptClientAsync, null);
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}