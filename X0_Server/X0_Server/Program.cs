using System;
using System.Net;
using System.Net.Sockets;

namespace X0_Server
{
    class Program
    {
        async void Recive()
        {
            byte[] dataReceived = new byte[1024];
            int bytesRead = await stream.ReadAsync(dataReceived, 0,dataReceived.Length);
        }
        async static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(ip, 1337);
            server.Start();
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                NetworkStream stream = client.GetStream();

            }
            
        }
    }
}
