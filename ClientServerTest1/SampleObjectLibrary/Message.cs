using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleObjectLibrary
{
    [Serializable]
    public class Message
    {
        public string ClientName { get; set; }
        public string MessageContent { get; set; }
        public Message(string MessageContent)
        {
            this.MessageContent = MessageContent;
        }
    }
}
