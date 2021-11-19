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
        private bool play = true;
        public StateOfGame(Game game) : base(game)
        {
        }
        private bool turn;
        public async override Task Handle()
        {
            Console.WriteLine("Game"); 
            turn = _game.IsWeX;
            GameOutput(_game.map);
            while (play)
            {
                
                if (turn)
                {
                    int number = Ask(_game.IsWeX);
                    await _game.Send($"2|{number}");
                }
                else
                {
                    Console.WriteLine("Ждём ход противника...");
                    turn = !turn;
                }
                Pars(await _game.Recive());
                GameOutput(_game.map);
            }
            Console.WriteLine("-----");
        }
        private void Pars(string text)
        {
            string[] message = text.Split('|'); //get value of recived message
            string answerCode = message[0];
            KnowledgeCenter knowledgeCenter = KnowledgeCenter.getInstance();
            if (answerCode == knowledgeCenter.codeOfMovement)
            {
                if (_game.IsWeX)
                {
                    if (message[1] == _game.Id.ToString())
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
                    if (answerCode == _game.Id.ToString())
                    {
                        _game.map[Convert.ToInt32(message[2])] = 2;
                    }
                    else
                    {
                        _game.map[Convert.ToInt32(message[2])] = 1;
                    }
                }
                if(message[1] == _game.Id.ToString())
                {
                    turn = false;
                }
                else
                {
                    turn = true;
                }
            }
            else if(answerCode == knowledgeCenter.codeOfWin)
            {
                if (message[1] == _game.Id.ToString())//TODO somehow catch movement
                {
                    Console.WriteLine("Вы выиграли!");
                }
                else
                {
                    Console.WriteLine("Вы проиграли!");
                }
                play = false;
                _game.ConditionState = new StateOfMenu(_game);
            }
            else if(answerCode == knowledgeCenter.codeOfDraw)
            {
                Console.WriteLine("Игра закончилась ничьёй!");
            }
            else
            {
                Console.WriteLine("Ошибка!");
            }
        }
    private void GameOutput(int[] map)
        {
            int turnOfPrint = 0;
            for (int i = 0; i < 9; i++)//go throught lines
            {
                if(turnOfPrint == 0)
                {
                    Console.Write("|");
                }
                
                if (map[i] == 1) //if unit equal number of X
                {
                    Console.Write("X|"); //X output
                }
                else if (map[i] == 2)//if unit equal number of 0
                {
                    Console.Write("0|");//0 output
                }
                else //if unit is empty
                {
                    Console.Write($" |");//empty output
                }
                turnOfPrint++;
                if(turnOfPrint == 3)
                {
                    Console.WriteLine("");//move to next line
                    turnOfPrint = 0;
                }
            }
        }
        private int Ask(bool x)
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
