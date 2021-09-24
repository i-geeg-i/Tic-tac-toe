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
            Console.WriteLine(Encoding.ASCII.GetString(buffer)); //debug
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
        static void Main(string[] args)
        {
            Socket sock = new Socket(
            AddressFamily.InterNetwork, 
            SocketType.Stream, 
            ProtocolType.Tcp 
            ); //make socket for recive and send
            IPAddress ip = IPAddress.Parse("127.0.0.1"); //chose ip
            IPEndPoint addr = new IPEndPoint(ip, 1337); //chose addres
            sock.Connect(addr);//connect addres and socket
            int[] map = new int[9]; //creating map of the game (maybe it should be in another class)
            while (true)
            {
                Console.WriteLine("1 - новая игра\n2 - список игр\n3 - подключиться к игре");//game menu output
                int enteredValue = Convert.ToInt32(Console.ReadLine()); //get value of person chose
                while (enteredValue > 3 || enteredValue < 1) //if value is incorrect
                {
                    Console.WriteLine("Вы ввели некорректное значение"); //person mistake output
                    Console.WriteLine("1 - новая игра\n2 - список игр\n3 - подключиться к игре");//game menu output
                    enteredValue = Convert.ToInt32(Console.ReadLine());//get value of person chose
                }
                switch (enteredValue)
                {
                    case 1: //if person want to create new game
                        Send("0", sock); // send creat code to server
                        break;
                    case 2: //if person want to have list of games
                        Send("1", sock); // send list code to server
                        break;
                    case 3: //if person want to connect to the game
                        Console.WriteLine("Введите id игры: "); //ask game id 
                        int id = Convert.ToInt32(Console.ReadLine()); //get value of id
                        while (id < 10000 || id > 99999) //while id is incorrect
                        {
                            Console.WriteLine("Выввели некорректное значение"); //person mistake output
                            Console.WriteLine("Введите id игры: "); //ask game id 
                            id = Convert.ToInt32(Console.ReadLine());//get value of id
                        }
                        Send(id.ToString(), sock); //send connect code to server
                        break;
                }
                Pars(Recive(sock)); //recive and generate a response
            }
        }
    }
}
