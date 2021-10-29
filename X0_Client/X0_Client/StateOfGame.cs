using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{class StateOfGame : State
    {
        
        public override void Handle(Game game)
        {
            Console.WriteLine("Game");
            bool play = true;
            while (play)
            {
                game.Recive();
                GameOutput(game.map);
                int number = Ask(game.IsWeX);
                game.Send();
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
