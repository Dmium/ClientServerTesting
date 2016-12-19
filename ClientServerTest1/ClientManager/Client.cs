using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Packets;

namespace ClientManager
{
    public class Client
    {
        private ClientThreadManager clm;
        public bool CanRead { get { return clm.CanRead; } }
        public Client(String ip, int port)
        {
            clm = new ClientThreadManager(ip, port);
        }
        public bool Read(out object obj)
        {
            DecodedPacket dp;
            if (!clm.Packets.IsEmpty)
                if (clm.Packets.TryDequeue(out dp))
                {
                    obj = dp.obj;
                    return true;
                }
            obj = null;
            return false;
        }
        public void Write(object obj)
        {
            clm.SendObject(obj);
        }
    }
}
