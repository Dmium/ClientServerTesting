using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener ConnectionListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 25566);
            ConnectionListener.Start();
            while (true)
            {
                while (!ConnectionListener.Pending())
                {
                    ConnectionListener.AcceptTcpClient();
                }
            }
        }
    }
}
