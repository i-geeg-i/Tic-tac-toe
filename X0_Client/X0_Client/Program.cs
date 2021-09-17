using System;
using System.Net;
using System.Net.Sockets;

namespace X0_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("127.0.0.1", 1337);
            string test = "123456789";
            while (true && 1 ==  - 1)
            {
                /*
                TcpClient client = await server.AcceptTcpClientAsync();
                NetworkStream stream = client.GetStream();
                byte[] dataReceived = new byte[4]; //string
                await stream.ReadAsync(dataReceived, 0, dataReceived.Length);
                Console.WriteLine(dataReceived.ToString());
                int toSend = Fib(Convert.ToInt32(Encoding.ASCII.GetString(dataReceived)));
                byte[] dataToSend = Encoding.ASCII.GetBytes(toSend.ToString());
                await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
                Console.WriteLine("Se*nt");
                stream.Close();
                */
            }
            Console.WriteLine(test.Length * sizeof(Char));
        }
    }
}
