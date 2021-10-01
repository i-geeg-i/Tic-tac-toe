using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace X0_Client
{
    class Program
    {
        public static Socket sock = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp
            ); //make socket for recive and send
        public static void Send(string text)  //function that send data to server
        {
            byte[] buffer;
            byte[] bufferText;
            byte[] dataToSend = Encoding.ASCII.GetBytes(text); //set value for buffer to send to server
            bufferText = Encoding.ASCII.GetBytes(String.Format("{0:000}", dataToSend.Length));
            buffer = new byte[bufferText.Length + dataToSend.Length];
            bufferText.CopyTo(buffer, 0);
            dataToSend.CopyTo(buffer, bufferText.Length);
            int totalSent = 0;  //variable that shows value of number sent data
            Console.WriteLine(buffer.Length);
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
        public static string Recive() //function that recive data from server
        {
            byte[] buffer = new byte[4096];   //place for bytes that we will get after recive
            int totalReceivedLen = 0;  //variable that shows value of number recived part of data
            while (totalReceivedLen < 3)   //running until all part of data will be recived
            {
                int actuallyReceived = sock.Receive(
                buffer,
                totalReceivedLen,
                buffer.Length - totalReceivedLen,
                SocketFlags.None
                );  //reciving data
                //error there
                Console.WriteLine(Encoding.ASCII.GetString(buffer)); //debug
                totalReceivedLen += actuallyReceived;  //increasing the value of number recived data
            }
            int realLength = Convert.ToInt32(Encoding.ASCII.GetString(buffer));
            byte[] realBuffer = new byte[realLength];
            int totalReceived = 0;
            while (totalReceived < realLength)   //running until all data will be recive
            {
                int actuallyReceived = sock.Receive(
                realBuffer,
                totalReceivedLen,
                realBuffer.Length - totalReceivedLen,
                SocketFlags.None
                );  //reciving data
                //error there
                Console.WriteLine(Encoding.ASCII.GetString(realBuffer)); //debug
                totalReceivedLen += actuallyReceived;  //increasing the value of number recived data
            }
            return Encoding.ASCII.GetString(realBuffer);
        }
        static void readerOfListOfGames(string text)    //analytic of recived list of games
        {
            string[] values = text.Split('.');  //get value of list
            for (int i = 0; i < values.Length; i++)    //go through recived list
            {
                Console.WriteLine(values[i]);   //value of list output 
            }
            
        }
        static void winResponse(int whoWin, bool x)
        {
                if ((x && whoWin == 1) ||(!x && whoWin == 2))
                {
                    Console.WriteLine("You win!");
                }
                else
                {
                    Console.WriteLine("You lose!");
                }     
        }
        public static void Pars(string text, ref bool X)   //function that generate a response
        {
            string[] message = text.Split('|'); //get value of recived message
            switch (message[0])
            {
                case "1":   //response if we get id of game(maybe because we ask to create new game or to connect to exist game)
                    
                    Console.WriteLine(Convert.ToInt32(message[1])); //id output
                    Console.WriteLine(Convert.ToInt32(message[2])); //x or 0; if 1 => x else if 2 => 0
                    if (Convert.ToInt32(message[2]) == 1)
                    {
                        X = true;
                    }
                    else if(Convert.ToInt32(message[2]) == 2)
                    {
                        X = false;
                    }

                    break;
                case "2":   //response if we get list of avaliable games 
                    readerOfListOfGames(message[1]); //game list output
                    break;
                case "3":   //response if we get some information about game change (for example move)
                    //TODO:Game response
                    break;
                case "4":   //response if we get information about sb win
                    winResponse(Convert.ToInt32(message[1]), X);
                    break;
                default:
                    Console.WriteLine("Error");
                    break;

            }
        }
        static void gameOutput(int[] map)
        {
            for (int lineNumber = 0; lineNumber < 3; lineNumber++)//go throught lines
            {
                Console.Write("|");
                for (int columnNumber = 0; columnNumber < 3; columnNumber++)//go throught columns
                {
                    if (map[lineNumber+columnNumber] == 1) //if unit equal number of X
                    {
                        Console.Write("X|"); //X output
                    }
                    else if(map[lineNumber + columnNumber] == 2)//if unit equal number of 0
                    {
                        Console.Write("0|");//0 output
                    }
                    else //if unit is empty
                    {
                        Console.Write($" |");//empty output
                    }
                    
                }
                Console.WriteLine("");//move to next line
            }
        }
        static int ask(bool x)
        {
            if (x) 
            {
                Console.WriteLine("Введите номер ячейки в которой хотите поставить X");
            }
            else
            {
                Console.WriteLine("Введите номер ячейки в которой хотите поставить 0");
            }
            
            int value = Convert.ToInt32(Console.ReadLine());
            while (value < 1 || value > 10)
            {
                Console.WriteLine("Вы ввели не правильное значение!");
                if (x)
                {
                    Console.WriteLine("Введите номер ячейки в которой хотите поставить X");
                }
                else
                {
                    Console.WriteLine("Введите номер ячейки в которой хотите поставить 0");
                }

                value = Convert.ToInt32(Console.ReadLine());
            }
            return value-1;
        }
        static void Main(string[] args)
        {
            State ConditionState = new StateOfMenu();

            IPAddress ip = IPAddress.Parse("127.0.0.1"); //chose ip
            IPEndPoint addr = new IPEndPoint(ip, 1337); //chose addres
            sock.Connect(addr);//connect addres and socket
            bool x = false;
            bool wePlayNow = false;
            int[] map = new int[9]; //creating map of the game (maybe it should be in another class)
            while (true)
            {
                ConditionState.Handle();
                if (!wePlayNow)
                {
                    
                }
                else
                {


                }
            }
        }
    }
}
