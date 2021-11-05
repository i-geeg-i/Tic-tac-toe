using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    abstract class State
    {
        public Game _game;
        public abstract Task Handle();
        public State(Game game)
        {
            _game = game;
        }
    }
}
