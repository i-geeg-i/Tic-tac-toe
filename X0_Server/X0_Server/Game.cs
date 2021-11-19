using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Server
{
    class Game
    {
        public Player player_who_is_X { get; set; } //x = 1
        public Player player_who_is_0 { get; set; } //y = 2
        public List<Player> players = new List<Player>();
        private int[] map = new int[9];
        public int id { get; }
        public bool started { get; set; } = false;
        public bool xMove = true;
        public Game(Player FirstPlayer)
        {
            id = CreateId();
            AddPlayer(FirstPlayer);            
        }
        public async Task AddPlayer(Player player)
        {
            if(players.Count < 2)
            {
                players.Add(player);
            }
            if (players.Count == 1)
            {
                
                Random random = new Random();
                
                if (random.Next(0, 1) == 0)
                {
                    player_who_is_X = player;
                    await player.Send($"1|{id}|1");
                }
                else
                {
                    player_who_is_0 = player;
                    await player.Send($"1|{id}|2");
                } 
            }
            else if(players.Count == 2)
            {
                if(player_who_is_X == null)
                {
                    player_who_is_X = player;
                    await player.Send($"1|{id}|1");
                }
                else
                {
                    player_who_is_0 = player;
                    await player.Send($"1|{id}|2");
                }
                started = true;
                await SendAllAboutConnection();
            }
        }
        private int FindWinner(int[] map)
        {
            //TODO: check 
            if (map[0] == 1 && map[1] == 1 && map[2] == 1)
            {
                return 1; //X is winner 
            }
            else if (map[0] == 2 && map[1] == 2 && map[2] == 2)
            {
                return 2; //Y is winner
            }
            else
            {
                return 0; //no one win
            }
        }
        private static int CreateId()
        {
            Random random = new Random();
            return random.Next(10000, 99999); 
        }
        private async Task SendAllAboutConnection()
        {
            var tasks = new Task[2];
            tasks[0] = player_who_is_X.Send("5|"); 
            tasks[1] = player_who_is_0.Send("5|");
            Task.WaitAll(tasks);
            foreach (var task in tasks)
            {
                Console.WriteLine(task.Status);
            }
        }
        private async Task SendAllAboutDraw()
        { 
            var tasks = new Task[2];
            tasks[0] = player_who_is_0.Send($"7|"); 
            tasks[1] = player_who_is_X.Send($"7|");
            Task.WaitAll(tasks);
        }
        private async Task SendAllAboutWinner(Player winer)
        {
            var tasks = new Task[2];
            tasks[0] = player_who_is_0.Send($"4|{winer.id}");
            tasks[1] = player_who_is_X.Send($"4|{winer.id}");
            Task.WaitAll(tasks);
        }
        private async Task SendAllMovement(Player playerWhoMove, int numberOfCell)
        {
            string movement = "";
            for (int i = 0; i < map.Length-1; i++)
            {
                movement += map[i].ToString();
                movement += ".";
            }
            movement += map[map.Length-1].ToString();
            var tasks = new Task[2];
            tasks[0] = player_who_is_0.Send($"3|{playerWhoMove.id}|{numberOfCell}"); //Can it be like this?
            tasks[1] = player_who_is_X.Send($"3|{playerWhoMove.id}|{numberOfCell}");
            Task.WaitAll(tasks);
        }
        public async Task SetX(int number)
        {
            if (number >= 0  && number <= 8 && map[number] == 0)
            {
                map[number] = 1;
                int winer = FindWinner(map);
                if (winer == 1)
                {
                    Console.WriteLine("X win!");
                    await SendAllAboutWinner(player_who_is_X);
                }
                else if(winer == 2)
                {
                    Console.WriteLine("0 win!");
                    await SendAllAboutWinner(player_who_is_0);
                }
                await SendAllMovement(player_who_is_X,number);
                xMove = !xMove;
            }
        }

        public async Task Set0(int number)
        {
            if (number >= 0 && number <= 8 && map[number] == 0)
            {
                map[number] = 2;
                int winner = FindWinner(map);
                await SendAllMovement(player_who_is_0, number);
                if (winner == 1)
                {
                    Console.WriteLine("X win!");
                    await SendAllAboutWinner(player_who_is_X);
                }
                else if (winner == 2)
                {
                    Console.WriteLine("0 win!");
                    await SendAllAboutWinner(player_who_is_0);
                }
                else if (winner == 3)
                {
                    Console.WriteLine("No one win!");
                    await SendAllAboutDraw();
                }
                xMove = !xMove;
            }
        }
    }
}
