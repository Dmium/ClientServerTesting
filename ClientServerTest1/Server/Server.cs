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
        const string ip = "127.0.0.1";//Should probably have a constructor for this
        const int port = 25566;
        Thread ServerThread;
        //Should probably make this a dictionary or something so the server require
        //a restart every 2,147,483,647 connections (and maybe give my ram a break)
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
                while (ConnectionListener.Pending())
                {
                    Managers.Add(new ClientThreadManager(ConnectionListener.AcceptTcpClient(), Managers.Count));
                }
                for (int i = 0; i < Managers.Count; i++)
                {
                    if (Managers[i].Connected)
                    {
                        while (!Managers[i].Packets.IsEmpty)
                        {
                            //Yeah this is bad. If TryDequeue consistantly fails then the entire server crashes :/
                            if (Managers[i].Packets.TryDequeue(out cPacket))
                                ReceivedPackets.Enqueue(cPacket);
                        }
                    }
                    else
                    {
                        Managers.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
        /// <summary>
        /// Gets the next preloaded packet. Will return false if it fails (and dp will be null)
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public bool GetPacket(out int ClientID, out object obj)
        {
            DecodedPacket dp;
            if (CanRead)
                if (ReceivedPackets.TryDequeue(out dp))
                {
                    ClientID = dp.ClientID;
                    obj = dp.obj;
                    return true;
                }
            ClientID = 0;
            obj = null;
            return false;
        }
        public void Broadcast(object obj)
        {
            foreach (ClientThreadManager manager in Managers)
                manager.SendObject(obj);
        }
    }
}
