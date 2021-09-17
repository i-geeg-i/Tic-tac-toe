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
            /*к приему-передачи:
             * int имя
             * int состояние ({0 - новая игра},{1 - список},{(5-ти значный int - идентификатор игры)})
             * сревер:
             * идентификатор новой игры
             * или
             * список игр
             */
            List<Game> games = new List<Game>();
            Dictionary<TcpClient, Player> players = new Dictionary<TcpClient, Player>();
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
