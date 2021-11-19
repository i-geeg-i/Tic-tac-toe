using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class StateOfWating : State
    {
        public StateOfWating(Game game) : base(game)
        {
        }
        public async override Task Handle()
        {
            Console.WriteLine("Ожидание подключения противника...");
            Pars(await _game.Recive());

        }
        private void Pars(string RecivedText)
        {
            string[] message = RecivedText.Split('|'); //get value of recived message
            string answerCode = message[0];
            if (answerCode == KnowledgeCenter.getInstance().codeOfGameStart)
            {
                _game.ConditionState = new StateOfGame(_game);
            }
            else
            {
                Console.WriteLine("Ошибка!");
            }
        }
    }
}
