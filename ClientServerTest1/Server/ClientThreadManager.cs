using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ServerManager
{
    public class ClientThreadManager
    {
        TcpClient Client;
        Thread Listener;
        NetworkStream Stream;
        byte[] dataBuffer;
        byte[] data;
        public ClientThreadManager(TcpClient Client)
        {
            this.Client = Client;
            Listener = new Thread(new ThreadStart(ClientListener));
            Listener.Start();
        }
        public void ClientListener()
        {
            Console.WriteLine("shit happened");
            try
            {
                while (Client.Connected)
                {
                    try
                    {
                        dataBuffer = new byte[2048];
                        Stream = Client.GetStream();
                        Stream.Read(dataBuffer, 0, dataBuffer.Length);
                        data = new byte[BitConverter.ToInt32(dataBuffer, 0)];
                        for (int i = 0; i < data.Length; i++)
                            data[i] = dataBuffer[i+4];
                        Console.WriteLine(System.Text.Encoding.ASCII.GetString(data));
                        Console.WriteLine("thing happened");
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
