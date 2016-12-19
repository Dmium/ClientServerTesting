using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Packets;
using System.Collections.Concurrent;

namespace Packets
{
    public class ClientThreadManager
    {
        private TcpClient Client;
        private Thread Listener;
        private NetworkStream Stream;
        public ConcurrentQueue<DecodedPacket> Packets = new ConcurrentQueue<DecodedPacket>();
        public int ClientID { get; }
        public bool CanRead { get { return !Packets.IsEmpty; } }
        public ClientThreadManager(TcpClient Client, int ClientID)
        {
            this.Client = Client;
            this.ClientID = ClientID;
            Listener = new Thread(new ThreadStart(ClientListener));
            Listener.Start();
        }
        /// <summary>
        /// Only for use by clients. This will mess up servers
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public ClientThreadManager(string ip, int port)
        {
            this.Client = new TcpClient();
            this.Client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            this.ClientID = 0;
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
                        if (Client.GetStream().CanRead)
                            Packets.Enqueue(PacketMethods.PacketToDecodedPacket(Client.GetStream(), ClientID));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Fucked up. " + e.Message);
                    }
                }
            }
            catch { }
        }
        public void SendObject(object obj)
        {
            byte[] byteToSend = PacketMethods.ObjectToPacket(obj);
            Client.GetStream().WriteAsync(byteToSend,0, byteToSend.Length);//I probably shouldn't have this running at the same time as the receiver but w/e it will probably maybe work
        }
    }
}
