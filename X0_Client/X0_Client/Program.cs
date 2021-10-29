using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace X0_Client
{
    class Program
    {
        
        
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1"); //chose ip
            IPEndPoint addr = new IPEndPoint(ip, 1337); //chose addres
            new Game(addr).Run();
        }
    }
}
