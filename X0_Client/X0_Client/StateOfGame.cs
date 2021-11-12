using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class StateOfGame : State
    {
        public StateOfGame(Game game) : base(game)
        {
        }
        public async override Task Handle()
        {
            Console.WriteLine("Game");
            bool play = true;
            bool turn = _game.IsWeX;
            while (play)
            {
                GameOutput(_game.map);
                if (turn)
                {
                    int number = Ask(_game.IsWeX);
                    await _game.Send($"2|{number}");
                    
                }
                Console.WriteLine("Waiting for enemy..."); ///TODO somehow make beter
                Pars(await _game.Recive());
            }
            
            
        }
        void Pars(string text)
        {
            string[] message = text.Split('|'); //get value of recived message
            if (message[0] == "3")
            {
                if (_game.IsWeX)
                {
                    if (message[1] == _game.sock.ToString())//TODO somehow catch movement
                    {
                        _game.map[Convert.ToInt32(message[2])] = 1;
                    }
                    else
                    {
                        _game.map[Convert.ToInt32(message[2])] = 2;
                    }
                }
                else
                {
                    if (message[1] == _game.sock.ToString())//TODO somehow catch movement
                    {
                        _game.map[Convert.ToInt32(message[2])] = 2;
                    }
                    else
                    {
                        _game.map[Convert.ToInt32(message[2])] = 1;
                    }
                }

                
            }
            else if(message[0] == "4")
            {
                if (message[1] == _game.sock.ToString())//TODO somehow catch movement
                {
                    Console.WriteLine("You win!");
                }
                else
                {
                    Console.WriteLine("You lost!");
                }
                _game.ConditionState = new StateOfMenu(_game);
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
        void GameOutput(int[] map)
        {
            for (int lineNumber = 0; lineNumber < 3; lineNumber++)//go throught lines
            {
                Console.Write("|");
                for (int columnNumber = 0; columnNumber < 3; columnNumber++)//go throught columns
                {
                    if (map[lineNumber + columnNumber] == 1) //if unit equal number of X
                    {
                        Console.Write("X|"); //X output
                    }
                    else if (map[lineNumber + columnNumber] == 2)//if unit equal number of 0
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
        int Ask(bool x)
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
            return value - 1;
        }


    }
    
}
