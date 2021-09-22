﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace X0_Client
{
    class Program
    {
        static void Send(string text, Socket sock)
        {
            byte[] buffer = new byte[8];
            buffer = Encoding.ASCII.GetBytes(text);
            int totalSent = 0;
            while (totalSent < buffer.Length)
            {
                int actuallySent = sock.Send(
                buffer,
                totalSent,
                buffer.Length - totalSent,
                SocketFlags.None
                );
                totalSent += actuallySent;
            }
        }
        static void Main(string[] args)
        {
            string test = "123456789";
            Socket sock = new Socket(
            AddressFamily.InterNetwork, 
            SocketType.Stream, 
            ProtocolType.Tcp 
            );
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint addr = new IPEndPoint(ip, 1337);
            sock.Connect(addr);
            while (true && 1 ==  - 1)
            {
                Console.WriteLine("1 - новая игра\n2 - список игр\n3 - подключиться к игре");
                int enteredValue = Convert.ToInt32(Console.ReadLine());
                while (enteredValue > 3 || enteredValue < 1)
                {
                    Console.WriteLine("Выввели некорректное значение");
                    Console.WriteLine("1 - новая игра\n2 - список игр\n3 - подключиться к игре");
                    enteredValue = Convert.ToInt32(Console.ReadLine());
                }
                switch (enteredValue)
                {
                    case 1:
                        Send("0", sock);
                    case 2:
                        Send("1", sock);
                    case 3:
                        Console.WriteLine("Введите id игры: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        while (id < 10000 || id > 99999)
                        {
                            Console.WriteLine("Выввели некорректное значение");
                            Console.WriteLine("Введите id игры: ");
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        Send(id.ToString(), sock);
                }
            }
            Console.WriteLine(test.Length * sizeof(Char));
        }
    }
}
