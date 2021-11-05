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
        void Pars(string text)
        {
            string[] message = text.Split('|'); //get value of recived message
            if(message[0] == "2")
            {
                 readerOfListOfGames(message[1]);
            }
            else 
            {
                Console.WriteLine("Error");
            }
        }
        void readerOfListOfGames(string text)    //analytic of recived list of games
        {
            string[] values = text.Split('.');  //get value of list
            for (int i = 0; i < values.Length; i++)    //go through recived list
            {
                Console.WriteLine(values[i]);   //value of list output 
            }

        }
    }
}
