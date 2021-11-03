using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class StateOfConnecting : State
    {
        public override void Handle(Game game)
        {
            Console.WriteLine("Connecting to the game");
            Console.WriteLine("Введите id игры: "); //ask game id 
            int id = Convert.ToInt32(Console.ReadLine()); //get value of id
            while (id < 10000 || id > 99999) //while id is incorrect
            {
                Console.WriteLine("Выввели некорректное значение"); //person mistake output
                Console.WriteLine("Введите id игры: "); //ask game id 
                id = Convert.ToInt32(Console.ReadLine());//get value of id
            }
            game.Send($"3|{id.ToString()}"); //send connect code to server
            Pars(game.Recive());
            game.ConditionState = new StateOfGame();
        }
        void Pars(string text)
        {
            string[] message = text.Split('|'); //get value of recived message
            Console.WriteLine(Convert.ToInt32(message[1])); //id output
            Console.WriteLine(Convert.ToInt32(message[2])); //x or 0; if 1 => x else if 2 => 0
            if (Convert.ToInt32(message[2]) == 1)
            {
                game.IsWeX = true;
            }
            else if (Convert.ToInt32(message[2]) == 2)
            {
                game.IsWeX = false;
            }
            else
            {
                Console.WriteLine("Error");
            }
        }
    }
}
