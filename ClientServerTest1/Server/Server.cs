using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerManager
{
    public class Server
    {
        const string ip = "127.0.0.1";
        const int port = 25566;
        Thread ServerThread;
        List<ClientThreadManager> Managers = new List<ClientThreadManager>();
        public void Start()
        {
            ServerThread = new Thread(Run);
            ServerThread.Start();
        }
        public void Run()
        {
            TcpListener ConnectionListener = new TcpListener(IPAddress.Parse(ip), port);
            ConnectionListener.Start();
            while (true)
            {
                while (!ConnectionListener.Pending())
                {
                    Managers.Add(new ClientThreadManager(ConnectionListener.AcceptTcpClient()));
                }
            }
        }
    }
}
