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

namespace ServerManager
{
    public class ClientThreadManager
    {
        private TcpClient Client;
        private Thread Listener;
        private NetworkStream Stream;
        public ConcurrentQueue<DecodedPacket> Packets = new ConcurrentQueue<DecodedPacket>();
        public int ClientID { get; }
        public ClientThreadManager(TcpClient Client, int ClientID)
        {
            this.Client = Client;
            this.ClientID = ClientID;
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
    }
}
