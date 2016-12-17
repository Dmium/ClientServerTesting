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
        byte[] data;
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
                while (Client.Connected)
                {
                    try
                    {
                        data = new byte[2048];
                        Stream = Client.GetStream();
                        Stream.Read(data, 0, data.Length);
                        Console.WriteLine(System.Text.Encoding.ASCII.GetString(data));
                    }
                    catch (Exception e){
                        Console.WriteLine("fucked up " + e.Message);
                    }
                }
            }
            catch { }
        }
    }
}
