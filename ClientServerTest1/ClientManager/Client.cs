using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ClientManager
{
    public class Client
    {
        TcpClient Con;
        Thread Listener;
        NetworkStream Stream;
        byte[] data;
        byte[] tempData;
        string stringToSend;
        public Client(String ip, int port)
        {
            this.Con = new TcpClient();
            Listener = new Thread(new ThreadStart(ClientListener));
            Listener.Start();
        }
        public void ClientListener()
        {
            try
            {
                Con.Connect(IPAddress.Parse("127.0.0.1"), 25566);
                while (Con.Connected)
                {
                    try
                    {
                        stringToSend = Console.ReadLine();
                        tempData = System.Text.Encoding.ASCII.GetBytes(stringToSend);
                        data = new byte[tempData.Length + 4];
                        for (int i = 0; i < 4; i++)
                            data[i] = BitConverter.GetBytes(tempData.Length)[i];
                        for (int i = 0; i < tempData.Length; i++)
                            data[i + 4] = tempData[i];
                        Stream = Con.GetStream();
                        Stream.Write(data, 0, data.Length);
                        Stream.Flush();
                        Console.WriteLine("Sent (probably)");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Fucked up. " + e.Message);
                    }
                }
            }
            catch { }
        }
    }
}
