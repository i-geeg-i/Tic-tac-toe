using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class StateOfListOfGames : State
    {
        public override void Handle(Game game)
        {
            Console.WriteLine("List of games");
            game.Send("1|-1"); // send list code to server
            
        }  
        void Pars(string text)
        {
            string[] message = text.Split('|'); //get value of recived message
            if(message[0] == "2")
            {
                game.readerOfListOfGames(message[1]);
            }
            else 
            {
                Console.WriteLine("Error");
            }
        }
    }
}
