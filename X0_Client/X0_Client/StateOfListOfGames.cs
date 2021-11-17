using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class StateOfListOfGames : State
    {
        public StateOfListOfGames(Game game) : base(game)
        {
        }
        public async override Task Handle()
        {
            Console.WriteLine("List of games");
            await _game.Send("1"); // send list code to server
            Pars(await _game.Recive());
            _game.ConditionState = new StateOfMenu(_game);
        }
        private void Pars(string text)
        {
            string[] message = text.Split('|'); //get value of recived message
            string RecivedCodeOfAnswer = message[0];
            string RecivedListOfGamesInTextFormat = message[1];
            if (RecivedCodeOfAnswer == "2")
            {
                 readerOfListOfGames(GetListOfGames(RecivedListOfGamesInTextFormat));
            }
            else 
            {
                Console.WriteLine("Error");
            }
        }
        private string[] GetListOfGames(string text)
        {
            string[] values = text.Split('.');  //get value of list
            return values;
        }
        private void readerOfListOfGames(string[] values)    //analytic of recived list of games
        {
            if (values.Length == 2 && values[1] == "")
            {
                Console.WriteLine("В данный момент нет доступных игр!");
                Console.WriteLine("-----");
                return;
            }
            Console.WriteLine("Игры:");
            for (int i = 1; i < values.Length; i++)    //go through recived list
            {
                Console.WriteLine($"{i}. { values[i]}");   //value of list output 
            }
            Console.WriteLine("-----");
        }
    }
}
