using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Packets;
using System.Collections.Concurrent;

namespace ServerManager
{
    public class Server
    {
        const string ip = "127.0.0.1";
        const int port = 25566;
        Thread ServerThread;
        private List<ClientThreadManager> Managers = new List<ClientThreadManager>();
        private ConcurrentQueue<DecodedPacket> ReceivedPackets = new ConcurrentQueue<DecodedPacket>();
        public bool CanRead { get { return !ReceivedPackets.IsEmpty; } }
        public void Start()
        {
            ServerThread = new Thread(Run);
            ServerThread.Start();
        }
        public void Run()
        {
            TcpListener ConnectionListener = new TcpListener(IPAddress.Parse(ip), port);
            DecodedPacket cPacket;
            ConnectionListener.Start();
            while (true)
            {
                while (!ConnectionListener.Pending())
                {
                    Managers.Add(new ClientThreadManager(ConnectionListener.AcceptTcpClient(), Managers.Count));
                }
                for (int i = 0; i < Managers.Count; i++)
                {
                    while(!Managers[i].Packets.IsEmpty)
                    {
                        //Yeah this is bad. If TryDequeue consistantly fails then the entire server crashes :/
                        if (Managers[i].Packets.TryDequeue(out cPacket))
                            ReceivedPackets.Enqueue(cPacket);
                    }
                }
            }
        }
    }
}
