using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class StateOfMenu : State
    {
        public StateOfMenu(Game game):base(game)
        {
        }
        public async override Task Handle()
        {
            Console.WriteLine("Menu");
            Console.WriteLine("1 - новая игра\n2 - список игр\n3 - подключиться к игре");//game menu output
            int enteredValue = Convert.ToInt32(Console.ReadLine()); //get value of person chose
            Console.WriteLine("-----");
            while (enteredValue > 3 || enteredValue < 1) //if value is incorrect
            {
                Console.WriteLine("Вы ввели некорректное значение"); //person mistake output
                Console.WriteLine("1 - новая игра\n2 - список игр\n3 - подключиться к игре");//game menu output
                enteredValue = Convert.ToInt32(Console.ReadLine());//get value of person chose
                Console.WriteLine("-----");
            }
            switch (enteredValue)
            {
                case 1: //if person want to create new game
                    _game.ConditionState = new StateOfNewGame(_game);
                    break;
                case 2: //if person want to have list of games
                    _game.ConditionState = new StateOfListOfGames(_game);
                    break;
                case 3: //if person want to connect to the game
                    _game.ConditionState = new StateOfConnectingToGame(_game);
                    break;
            }
        }
    }
}
