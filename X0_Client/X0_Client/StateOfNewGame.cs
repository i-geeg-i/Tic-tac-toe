using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class StateOfNewGame : State
    {
        
        public override void Handle(Game game)
        {
            Console.WriteLine("New game");
            game.Send("0|-1"); // send creat code to server
        }
    }
}
