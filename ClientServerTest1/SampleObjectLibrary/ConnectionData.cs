using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleObjectLibrary
{
    [Serializable]
    public class ConnectionData
    {
        public string ClientName { get; }
        public ConnectionData(string ClientName)
        {
            this.ClientName = ClientName;
        }
    }
}
