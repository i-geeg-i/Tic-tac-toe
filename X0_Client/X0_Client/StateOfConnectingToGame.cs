using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class StateOfConnectingToGame : State
    {
        public StateOfConnectingToGame(Game game) : base(game)
        {
        }
        public async override Task Handle()
        {
            Console.WriteLine("Connecting to the game");
            Console.WriteLine("Введите id игры: "); //ask game id 
            int id = Convert.ToInt32(Console.ReadLine()); //get value of id
            Console.WriteLine("-----");
            while (id < 10000 || id > 99999) //while id is incorrect
            {
                Console.WriteLine("Выввели некорректное значение"); //person mistake output
                Console.WriteLine("Введите id игры: "); //ask game id 
                id = Convert.ToInt32(Console.ReadLine());//get value of id
                Console.WriteLine("-----");
            }
            await _game.Send($"3|{id.ToString()}"); //send connect code to server
            Pars(await _game.Recive());
            _game.ConditionState = new StateOfWating(_game);
        }
        private void Pars(string text)
        {
            string[] message = text.Split('|'); //get value of recived message
            Console.WriteLine(Convert.ToInt32(message[0])); //id output
            string answerCode = message[0];
            if(answerCode == "666") //666- error code
            {
                Console.WriteLine("Введено не коректное число!");
            }
            Console.WriteLine(Convert.ToInt32(message[1])); //x or 0; if 1 => x else if 2 => 0
            if (Convert.ToInt32(message[1]) == 1)
            {
                _game.IsWeX = true;
            }
            else if (Convert.ToInt32(message[1]) == 2)
            {
                _game.IsWeX = false;
            }
            else
            {
                Console.WriteLine("Ошибка!");
            }
        }
    }
}
