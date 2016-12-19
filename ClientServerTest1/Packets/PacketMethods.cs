using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    public static class PacketMethods
    {
        public static NumberByteArray GetFirstInt(byte[] data)
        {
            int number = BitConverter.ToInt32(data, 0);
            byte[] tempData = new byte[data.Length - 4];
            for (int i = 0; i < tempData.Length; i++)
                tempData[i] = data[i + 4];
            return new NumberByteArray(number, tempData);
        }
        public static DecodedPacket PacketToDecodedPacket(NetworkStream stream, int clientID)
        {
            byte[] dataBuffer = new byte[4194304];
            stream.Read(dataBuffer, 0, dataBuffer.Length);
            return new DecodedPacket(clientID,(new BinaryFormatter()).Deserialize(new System.IO.MemoryStream(GetFirstInt(dataBuffer).ByteArray)));
        }
        private static byte[] ObjectToByteArray(Object obj)
        {
            MemoryStream temp = new MemoryStream();
            (new BinaryFormatter()).Serialize(temp, obj);
            return temp.ToArray();
        }
        public static byte[] ObjectToPacket(object obj)
        {
            byte[] tempByteArr = ObjectToByteArray(obj);
            List<byte> tempList = new List<byte>();
            tempList.AddRange(BitConverter.GetBytes(tempByteArr.Length));
            tempList.AddRange(tempByteArr);
            return tempList.ToArray();
        }
    }
}
