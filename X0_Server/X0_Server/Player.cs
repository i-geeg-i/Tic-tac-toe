using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace X0_Server
{
    class Player
    {
        public string Name { get; }
        public int wins { get; set; } = 0;
        public Player(string name)
        {
            Name = name;
        }
        async public static void Recive(TcpListener server, NetworkStream stream, Dictionary<TcpClient,Player> players)
        {
            TcpClient client = await server.AcceptTcpClientAsync();
            byte[] dataReceived = new byte[4];
            await stream.ReadAsync(dataReceived, 0, dataReceived.Length);
            if (players.ContainsKey(client))
            {

            }
            else
            {
                players.Add(client, new Player(Encoding.ASCII.GetString(dataReceived)));
            }
            string toSend = "";
            byte[] dataToSend = Encoding.ASCII.GetBytes(toSend.ToString());
            await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
            Console.WriteLine("Sent");
            //stream.Close();
        }

    }
}
