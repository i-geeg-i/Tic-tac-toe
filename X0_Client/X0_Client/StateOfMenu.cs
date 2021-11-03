using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class StateOfMenu : State
    {
        public override void Handle(Game game)
        {
            Console.WriteLine("Menu");
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
                    game.ConditionState = new StateOfNewGame();
                    break;
                case 2: //if person want to have list of games
                    game.ConditionState = new StateOfListOfGames();
                    break;
                case 3: //if person want to connect to the game
                    game.ConditionState = new StateOfConnecting();
                    break;
            }
            //game.Pars(game.Recive(), ref game.IsWeX); //recive and generate a response
        }
    }
}
