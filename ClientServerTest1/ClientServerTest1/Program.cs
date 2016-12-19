using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ServerManager;
using SampleObjectLibrary;
namespace ClientServerTest1
{
    class Program
    {
        static Dictionary<int, Client> clients = new Dictionary<int, Client>();
        static Server server = new Server();
        static int ClientID;
        static object obj;
        static void Main(string[] args)
        {
            server.Start();
            while (true)
            {
                System.Threading.Thread.Sleep(2);
                while (server.CanRead)
                {
                    TranslatePacket();
                }
            }
        }
        /// <summary>
        /// Only made this method to help with Garbage collection. Not sure if I made the problem better or worse.
        /// </summary>
        private static void TranslatePacket()
        {
            if (server.GetPacket(out ClientID, out obj))
            {
                if (obj == null)
                {

                }
                else if (obj.GetType() == typeof(ConnectionData))
                {
                    Console.WriteLine(((ConnectionData)obj).ClientName + " connected.");
                    if (clients.ContainsKey(ClientID))
                        clients.Remove(ClientID);
                    clients.Add(ClientID, new Client(ClientID, ((ConnectionData)obj).ClientName));
                }
                else if (obj.GetType() == typeof(Message))
                {
                    Console.WriteLine(clients[ClientID].Name + ": " + ((Message)obj).MessageContent);
                    server.Broadcast(new Message(clients[ClientID].Name + ": " + ((Message)obj).MessageContent));
                }
            }
        }
    }
    class Client
    {
        public int ID { get; }
        public string Name { get; }
        public Client(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
}
