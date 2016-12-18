using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Packets
{
    public class Packet
    {
        private int clientID;
        private object obj;
        public Packet(byte[] data)
        {
            NumberByteArray tempData = PacketMethods.GetFirstInt(data);
            clientID = tempData.Number;
            obj = \
            //clientID = BitConverter.ToInt32(data, 0);
            //byte[] tempData = new byte[data.Length - 4];
            //for (int i = 0; i < tempData.Length; i++)
            //    tempData[i] = data[i + 4];
        }
    }
}
