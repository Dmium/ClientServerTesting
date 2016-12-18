using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
