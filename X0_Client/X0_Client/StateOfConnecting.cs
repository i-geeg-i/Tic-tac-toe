using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class StateOfConnecting : State
    {
        public StateOfConnecting(Game game) : base(game)
        {
        }
        public async override Task Handle()
        {
           await _game.Send($"555"); //send connect code to server
            Pars(await _game.Recive());
            _game.ConditionState = new StateOfMenu(_game);
        }
        private void Pars(string text)
        {
            string[] message = text.Split('|'); //get value of recived message
            Console.WriteLine(Convert.ToInt32(message[0])); //id output
            if (message[0] == "666")
            {
                return;
            }
            else if (Convert.ToInt32(message[0]) == 6)
            {
                _game.Id = Convert.ToInt32(message[1]);
            }
            else
            {
                Console.WriteLine("Ошибка!");
            }

        }
    }
}
