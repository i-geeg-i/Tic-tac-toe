using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class StateOfNewGame : State
    {
        public StateOfNewGame(Game game) : base(game)
        {
        }
        public async override Task Handle()
        {
            Console.WriteLine("New game");
            await _game.Send("0"); // send creat code to server
            Pars(await _game.Recive());
            _game.ConditionState = new StateOfWating(_game);
        }
        private void Pars(string text)
        {
            string[] message = text.Split('|'); //get value of recived message
            Console.WriteLine($"id: {Convert.ToInt32(message[1])}"); //id output
            if (Convert.ToInt32(message[2]) == 1)
            {
                _game.IsWeX = true;
            }
            else if (Convert.ToInt32(message[2]) == 2)
            {
                _game.IsWeX = false;
            }
            else
            {
                Console.WriteLine("Ошибка!");
                _game.ConditionState = new StateOfMenu(_game);
            }
            Console.WriteLine("Подключено!");
            Console.WriteLine("-----");
        }
    }
}
