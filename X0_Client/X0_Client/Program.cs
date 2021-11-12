using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class Program
    {
        
        
        async static Task Main(string[] args)
        {
            await new Game().Run();
        }
    }
}
