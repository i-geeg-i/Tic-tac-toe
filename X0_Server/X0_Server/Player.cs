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
            Stream = Client.GetStream();
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
        public async Task PlayerHandle()
        {
            while (true)
            {
                string recivedString =  await Recive();
                string[] recivedValues = recivedString.Split('|');
                int comand = Convert.ToInt32(recivedValues[0]);
                int x = Convert.ToInt32(recivedValues[1]);
                Dictionary<TcpClient,Player> players = KnowledgeCenter.getInstance().players;
                if (!players.ContainsKey(Client))
                {
                    players.Add(Client, this);
                }
                if (comand == 0 && game == null)
                {
                    game = new Game(this);
                    Send($"1|{game.id}");

                }
                else if (comand == 1 && game == null)
                {
                    Send($"2|{KnowledgeCenter.getInstance().GetOpenGames()}");
                    Console.WriteLine($"2|{KnowledgeCenter.getInstance().GetOpenGames()}".Length * sizeof(Char));
                }
                else if (comand == 2 && game != null)
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
                    game = KnowledgeCenter.getInstance().FindGame(comand);
                    Send($"1|{game.id}");
                }
            }
           
        }
    
        async public Task<string> Recive()
        {

            byte[] dataReceived = new byte[4]; //place for bytes that we will get after recive
            int totalReceived = 0;  //variable that shows value of number recived data
            while (totalReceived < dataReceived.Length)   //running until all data will be recive
            {
               
                int actuallyReceived = await Stream.ReadAsync(dataReceived, totalReceived, dataReceived.Length - totalReceived); //reciving data
                totalReceived += actuallyReceived;  //increasing the value of number recived data
            }
            Console.WriteLine(Encoding.ASCII.GetString(dataReceived)); //debug
            return Encoding.ASCII.GetString(dataReceived); //return of recived string
        }
    }
}
