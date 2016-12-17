using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ClientManager;
namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
            Client client = new Client("127.0.0.1", 25566);
            System.Threading.Thread.Sleep(10000);
        }
    }
}
