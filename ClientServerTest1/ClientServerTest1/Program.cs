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
            Server server = new Server();
            server.Start();
            int ClientID;
            object obj;
            while (true)
                while (server.CanRead)
                {
                    if (server.GetPacket(out ClientID, out obj))
                    {
                        Console.WriteLine(ClientID + ": " + ((Message)obj).MessageContent);
                        server.Broadcast(((Message)obj));
                    }
                }
        }
    }
}
