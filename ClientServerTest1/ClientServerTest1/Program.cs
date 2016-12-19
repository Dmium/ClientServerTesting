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
        static void Main(string[] args)
        {
            Dictionary<int, Client> clients = new Dictionary<int, Client>();
            Server server = new Server();
            server.Start();
            int ClientID;
            object obj;
            while (true)
            {
                System.Threading.Thread.Sleep(2);
                while (server.CanRead)
                {
                    if (server.GetPacket(out ClientID, out obj))
                    {
                        if (obj == null)
                        {

                        }
                        else if (obj.GetType() == typeof(ConnectionData))
                        {
                            Console.WriteLine(((ConnectionData)obj).ClientName + " connected.");
                            clients.Add(ClientID, new Client(ClientID, ((ConnectionData)obj).ClientName));
                        }
                        else if (obj.GetType() == typeof(Message))
                        {
                            Console.WriteLine(clients[ClientID].Name + ": " + ((Message)obj).MessageContent);
                            server.Broadcast(new Message(clients[ClientID].Name + ": " + ((Message)obj).MessageContent));
                            break;
                        }
                    }
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
