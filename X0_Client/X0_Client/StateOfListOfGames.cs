using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class StateOfListOfGames : State
    {
        public override void Handle(Program program)
        {
            Console.WriteLine("List of games");
            program.Send("1|-1"); // send list code to server
        }  
    }
}
