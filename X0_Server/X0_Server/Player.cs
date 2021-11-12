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
        
        async public Task Send(string text)  //function that send data to server
        {
            /*
            * codes
            * {1} - id of game
            * {2} - list of games
            * {3} - movement of someone
            * {4} - someone is winer
            */
            byte[] buffer;  //place for bytes that we will get after encoding text
            byte[] dataToSend = Encoding.ASCII.GetBytes(text); //set value for buffer to send to server
            buffer = Encoding.ASCII.GetBytes(String.Format("{0:000}", dataToSend.Length));
            dataToSend.CopyTo(buffer, 3);
            Console.WriteLine(buffer.Length);
            await Stream.WriteAsync(buffer, 0, dataToSend.Length);

        }
        async public Task<string> Recive() //function that recive data from server
        {
            byte[] buffer = new byte[4096];   //place for bytes that we will get after recive
            int totalReceivedLen = 0;  //variable that shows value of number recived part of data
            while (totalReceivedLen < 3)   //running until all part of data will be recived
            {
                int actuallyReceived = await Stream.ReadAsync(buffer, totalReceivedLen, 3);
                Console.WriteLine(Encoding.ASCII.GetString(buffer)); //debug
                totalReceivedLen += actuallyReceived;  //increasing the value of number recived data
            }
            
            int realLength = Convert.ToInt32(Encoding.ASCII.GetString(buffer));
            byte[] realBuffer = new byte[realLength];
            int totalReceived = 0;
            while (totalReceived < realLength)   //running until all data will be recive
            {
                int actuallyReceived = await Stream.ReadAsync(realBuffer, totalReceived, realBuffer.Length - totalReceived);
                Console.WriteLine(Encoding.ASCII.GetString(realBuffer)); //debug
                totalReceived += actuallyReceived;  //increasing the value of number recived data
            }
            return Encoding.ASCII.GetString(realBuffer);
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
                    if(game.player_who_is_X == this)
                    {
                        await Send($"1|{game.id}|1");
                    }
                    else
                    {
                        await Send($"1|{game.id}|2");
                    }
                    

                }
                else if (comand == 1 && game == null)
                {
                    await Send($"2|{KnowledgeCenter.getInstance().GetOpenGames()}");
                    Console.WriteLine($"2|{KnowledgeCenter.getInstance().GetOpenGames()}");
                }
                else if (comand == 2 && game != null)
                {
                    if (game.player_who_is_0 == this)
                    {
                        if (game.Set0(x))
                        {
                            Send($"3|{Client}|{x}");//TODO client is not avaliable in client app
                        }
                    }
                    else if (game.player_who_is_X == this)
                    {
                        if (game.SetX(x))
                        {
                            await Send($"3|{Client}|{x}");
                        }
                    }
                }
                else if (comand == 3 && game == null)
                {
                    game = KnowledgeCenter.getInstance().FindGame(x);
                    await Send($"1|{game.id}");
                }
                else
                {
                    await Send($"5");
                }
            }
            Stream.Close();
        }
    }
}
