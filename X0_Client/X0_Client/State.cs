using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    abstract class State
    {
        public Game game;
        public abstract void Handle(Game game);
    }
}
