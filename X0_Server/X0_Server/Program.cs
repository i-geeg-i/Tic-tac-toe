﻿using System;
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
        static void Main(string[] args)
        {
            Main();
        }
        async static void Main()
        {
            Console.WriteLine("Hello World!");
            /*Recive:
             * int Condition (
             * {0 - new game},
             * {1 - list of avaliable games},
             * {2 - move},
             * {(5 numbers int - id of game)}
             * )
             * int x 4 
             */ 
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(ip, 1337);
            server.Start();
            while (true)
            {
                TcpClient client = await server.AcceptTcpClientAsync();
                NetworkStream stream = client.GetStream();
                new Player(client).Recive();
            }
            
        }
    }
}
