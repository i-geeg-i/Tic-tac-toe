using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace X0_Server
{
    class Program
    {
        async Task<int> Recive(NetworkStream stream)
        {
            byte[] dataReceived = new byte[1024];
            int bytesRead = await stream.ReadAsync(dataReceived, 0,dataReceived.Length);
            return dataReceived.Length;
        }
        async static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            /*к приему:
             * int состояние ({0 - новая игра},{1 - список},{2 - ход},{(5-ти значный int - идентификатор игры)}) 4
             * int x 4
             */ 
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(ip, 1337);
            server.Start();
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                NetworkStream stream = client.GetStream();
                new Player(client).Recive(stream);
            }
            
        }
    }
}
