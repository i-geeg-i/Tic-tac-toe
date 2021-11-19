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
        public int id;
        public Player(TcpClient client)
        {
            Client = client;
            Stream = Client.GetStream();
            id = KnowledgeCenter.getInstance().GetId();
        }
        
        async public Task Send(string text)  //function that send data to server
        {
            /*
            * codes
            * {1} - id of game
            * {2} - list of games
            * {3} - movement of someone
            * {4} - someone is winer
            * {5} - game started
            * {6} - new user
            * {7} - draw
            */
            byte[] bufferLen;  //place for bytes that we will get after encoding text
            byte[] dataToSendFromUser = Encoding.ASCII.GetBytes(text); //set value for buffer to send to server
            bufferLen = Encoding.ASCII.GetBytes(String.Format("{0:000}", dataToSendFromUser.Length));
            byte[] Result = new byte[bufferLen.Length + dataToSendFromUser.Length];
            bufferLen.CopyTo(Result, 0);
            dataToSendFromUser.CopyTo(Result, bufferLen.Length);
            Console.WriteLine($"sent: {text}");
            await Stream.WriteAsync(Result, 0, Result.Length);

        }
        async public Task<string> Recive() //function that recive data from server
        {
            byte[] buffer = new byte[4096];   //place for bytes that we will get after recive
            int totalReceivedLen = 0;  //variable that shows value of number recived part of data
            while (totalReceivedLen < 3)   //running until all part of data will be recived
            {
                int actuallyReceived = await Stream.ReadAsync(buffer, totalReceivedLen, 3);
                totalReceivedLen += actuallyReceived;  //increasing the value of number recived data
            }
            
            int realLength = Convert.ToInt32(Encoding.ASCII.GetString(buffer));
            byte[] realBuffer = new byte[realLength];
            int totalReceived = 0;
            while (totalReceived < realLength)   //running until all data will be recive
            {
                int actuallyReceived = await Stream.ReadAsync(realBuffer, totalReceived, realBuffer.Length - totalReceived);
                totalReceived += actuallyReceived;  //increasing the value of number recived data
            }
            Console.WriteLine(Encoding.ASCII.GetString(realBuffer));
            return Encoding.ASCII.GetString(realBuffer);
        }       
        public async Task PlayerHandle()
        {
            while (true)
            {
                string recivedString =  await Recive();
                string[] recivedValues = recivedString.Split('|');
                int comand = Convert.ToInt32(recivedValues[0]);
                int comandOfNewGame = 0;
                int comandOfListOfAvaliableGames = 1;
                int comandOfMove = 2;
                int comandOfConnectingToGame = 3;
                int x = -1;
                if(recivedValues.Length > 1)
                {
                    x = Convert.ToInt32(recivedValues[1]);
                }
                Dictionary<TcpClient,Player> players = KnowledgeCenter.getInstance().players;
                bool isThisFirstTime = false;
                if (!players.ContainsKey(Client))
                {
                    players.Add(Client, this);
                    await Send($"6|{id}");
                    Console.WriteLine($"6|{id}");
                    isThisFirstTime = true;
                }
                if (!isThisFirstTime)
                {
                    if (comand == comandOfNewGame && game == null)
                    {
                        game = new Game(this);
                        KnowledgeCenter.getInstance().games.Add(game);
                    }
                    else if (comand == comandOfListOfAvaliableGames && game == null)
                    {
                        await Send($"2|{KnowledgeCenter.getInstance().GetOpenGames()}");
                        Console.WriteLine($"2|{KnowledgeCenter.getInstance().GetOpenGames()}");
                    }
                    else if (comand == comandOfMove && game != null)
                    {
                        if (game.player_who_is_0 == this)
                        {
                            if (!game.xMove)
                            {
                                await game.Set0(x);
                            }

                        }
                        else if (game.player_who_is_X == this)
                        {
                            if (game.xMove)
                            {
                                await game.SetX(x);
                            }
                        }

                    }
                    else if (comand == comandOfConnectingToGame && game == null)
                    {
                        game = KnowledgeCenter.getInstance().FindGame(x, this);
                        game.AddPlayer(this);
                    }
                    else
                    {
                        await Send($"666");
                    }
                } 
            }
            Stream.Close();
        }
    }
}
