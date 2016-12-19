using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using ClientManager;
using SampleObjectLibrary;
using System.Threading;
namespace TestClient
{
    class Program
    {
        static Thread receiver = new Thread(ReceiveThread);
        static Client client;
        static void Main(string[] args)
        {
            Console.ReadLine();
            client = new Client("127.0.0.1", 25566);
            receiver.Start();
            Message messageToSend;
            while (true)
            {
                messageToSend = new Message(Console.ReadLine());
                client.Write(messageToSend);
            }
        }
        public static void ReceiveThread()
        {
            while (true)
                while (client.CanRead)
                {
                    object obj;
                    if (client.Read(out obj))
                        if (obj.GetType() == typeof(Message))
                            Console.WriteLine(((Message)obj).MessageContent);
                }
        }
    }
}
