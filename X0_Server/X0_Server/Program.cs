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
        async static Task Main()
        {
            Console.WriteLine("Запуск...");
            /*Recive:
             * int Condition (
             * {0 - new game},
             * {1 - list of avaliable games},
             * {2 - move},
             * {3(5 numbers int - id of game) - connect}
             */ 
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(ip, 1337);
            server.Start();
            Console.WriteLine("Всё запущено и готово к работе!");
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                new Player(client).PlayerHandle();
            }
            
        }
    }
}
