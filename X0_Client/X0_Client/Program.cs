using System;
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
        static string Recive(Socket sock)
        {
            byte[] buffer = new byte[18];
            int totalReceived = 0;
            while (totalReceived < buffer.Length)
            {
                int actuallyReceived = sock.Receive(
                buffer,
                totalReceived,
                buffer.Length - totalReceived,
                SocketFlags.None
                );

                totalReceived += actuallyReceived;
            }
            return Encoding.ASCII.GetString(buffer);
        }
        static string Pars(string text)
        {
            string[] mes = text.Split('|');
            int id = 0;
            switch (mes[0])
            {
                case 1:
                    id = Convert.ToInt32(mes[1]);
                case 2:
                //UNDONE: reading list of games
                case 3:
                //UNDONE:Move
                case 4:
                //UNDONE: Win

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
                Recive();
            }
            Console.WriteLine(test.Length * sizeof(Char));
        }
    }
}
