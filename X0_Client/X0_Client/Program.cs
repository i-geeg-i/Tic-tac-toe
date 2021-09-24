using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace X0_Client
{
    class Program
    {
        static void Send(string text, Socket sock)  //function that send data to server
        {
            byte[] buffer;  //place for bytes that we will get after encoding text
            buffer = Encoding.ASCII.GetBytes(text); //set value for buffer to send to server
            int totalSent = 0;  //variable that shows value of number sent data
            while (totalSent < buffer.Length)   //sending until we have sent all data 
            {
                int actuallySent = sock.Send(
                buffer,
                totalSent,
                buffer.Length - totalSent,
                SocketFlags.None
                );  //sending data
                totalSent += actuallySent;  //increasing the value of number sent data
            }
        }
        static string Recive(Socket sock) //function that recive data from server
        {
            byte[] buffer = new byte[18];   //place for bytes that we will get after recive
            int totalReceived = 0;  //variable that shows value of number recived data
            while (totalReceived < buffer.Length)   //running until all data will be recive
            {
                int actuallyReceived = sock.Receive(
                buffer,
                totalReceived,
                buffer.Length - totalReceived,
                SocketFlags.None
                );  //reciving data
                //error there
                totalReceived += actuallyReceived;  //increasing the value of number recived data
            }
            return Encoding.ASCII.GetString(buffer);
        }
        static void readerOfListOfGames(string text)    //analytic of recived list of games
        {
            string[] values = text.Split('.');  //get value of list
            for (int i = 0; i < values.Length; i++)    //go through recived list
            {
                Console.WriteLine(values[i]);   //value of list output 
            }
            
        }
        static void Pars(string text)   //function that generate a response
        {
            string[] message = text.Split('|'); //get value of recived message
            switch (message[0])
            {
                case "1":   //response if we get id of game(maybe because we ask to create new game or to connect to exist game)
                    Console.WriteLine(Convert.ToInt32(message[1])); //id output
                    break;
                case "2":   //response if we get list of avaliable games 
                    readerOfListOfGames(message[1]); //game list output
                    break;
                case "3":   //response if we get some information about game change (for example move)
                    //TODO:Game response
                    break;
                case "4":   //response if we get information about sb win
                    //TODO:Win response
                    break;

            }
        }
        static void Main(string[] args)
        {
            string test = "123456789";
            Console.WriteLine(System.Text.ASCIIEncoding.Unicode.GetByteCount("1|55555"));
            Console.WriteLine(System.Text.ASCIIEncoding.Unicode.GetByteCount("1"));
            Socket sock = new Socket(
            AddressFamily.InterNetwork, 
            SocketType.Stream, 
            ProtocolType.Tcp 
            );
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint addr = new IPEndPoint(ip, 1337);
            sock.Connect(addr);
            while (true)
            {
                Console.WriteLine("1 - новая игра\n2 - список игр\n3 - подключиться к игре");
                int enteredValue = Convert.ToInt32(Console.ReadLine());
                while (enteredValue > 3 || enteredValue < 1)
                {
                    Console.WriteLine("Вы ввели некорректное значение");
                    Console.WriteLine("1 - новая игра\n2 - список игр\n3 - подключиться к игре");
                    enteredValue = Convert.ToInt32(Console.ReadLine());
                }
                switch (enteredValue)
                {
                    case 1:
                        Send("0", sock);
                        break;
                    case 2:
                        Send("1", sock);
                        break;
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
                        break;
                }
                Pars(Recive(sock));
            }
            Console.WriteLine(test.Length * sizeof(Char));
        }
    }
}
