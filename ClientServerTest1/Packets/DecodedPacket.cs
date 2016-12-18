using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Packets
{
    public class DecodedPacket
    {
        public int ClientID { get; }
        public object obj { get; }
        public DecodedPacket()
        {

        }
        public DecodedPacket(int ClientID, object obj)
        {
            this.ClientID = ClientID;
            this.obj = obj;
        }
    }
}
