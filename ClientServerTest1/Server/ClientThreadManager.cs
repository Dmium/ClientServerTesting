using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    class ClientThreadManager
    {
        TcpClient Client;
        Thread Listener;
        NetworkStream Stream;
        public ClientThreadManager(TcpClient Client)
        {
            this.Client = Client;
            Listener = new Thread(new ThreadStart(ClientListener));
            Listener.Start();
        }
        public void ClientListener()
        {
            try
            {
                Stream = Client.GetStream();
                while (Client.Connected)
                {

                }
            }
            catch { }
        }
    }
}
