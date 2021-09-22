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
        public TcpClient Client { get; }
        public NetworkStream Stream { get; }
        public int Wins { get; set; } = 0;
        public Game game { get; set; }
        public Player(TcpClient client)
        {
            Client = client;
        }
        async public void Send(string text)
        {
            /*
             * codes
             * {1} - id of game
             * {2} - list of games
             * {3} - movement of someone
             * {4} - someone is winer
             */
            byte[] dataToSend = Encoding.ASCII.GetBytes(text.ToString());
            await Stream.WriteAsync(dataToSend, 0, dataToSend.Length);
            Console.WriteLine("Sent");
            Stream.Close();
        }
        public async Task PlayerHandler()
        {
            while (true)
            {
                string[] recivedValues = Recive().Result.Split('|');
                int sost = Convert.ToInt32(recivedValues[0]);
                int x = Convert.ToInt32(recivedValues[1]);
                Dictionary<TcpClient,Player> players = KnowledgeCenter.getInstance().players;
                if (!players.ContainsKey(Client))
                {
                    players.Add(Client, this);
                }
                if (sost == 0 && game == null)
                {
                    game = new Game(this);
                    Send($"1|{game.id}");
                }
                else if (sost == 1 && game == null)
                {
                    Send($"2|{KnowledgeCenter.getInstance().GetOpenGames()}");
                }
                else if (sost == 2 && game != null)
                {
                    if (game.player_who_is_0 == this)
                    {
                        if (game.Set0(x))
                        {
                            Send($"3|{Client}|{x}");
                        }
                    }
                    else if (game.player_who_is_X == this)
                    {
                        if (game.SetX(x))
                        {
                            Send($"3|{Client}|{x}");
                        }
                    }

                }
                else
                {
                    game = KnowledgeCenter.getInstance().FindGame(sost);
                    Send($"1|{game.id}");
                }
            }
           
        }
    
        async public Task<string> Recive()
        {

            byte[] dataReceived = new byte[4];
            await Stream.ReadAsync(dataReceived, 0, dataReceived.Length);
            string recivedString = Encoding.ASCII.GetString(dataReceived);
            return recivedString;
        }
            

    }
}
