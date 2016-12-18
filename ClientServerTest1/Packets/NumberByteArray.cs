using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    public class NumberByteArray
    {
        public int Number  { get; }
        public byte[] ByteArray { get; }
        public NumberByteArray(int Number, byte[] ByteArray)
        {
            this.Number = Number;
            this.ByteArray = ByteArray;
        }
    }
}
