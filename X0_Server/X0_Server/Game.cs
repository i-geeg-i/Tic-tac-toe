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
        public Game(Player FirstPlayer)
        {
            players.Add(FirstPlayer);
            id = CreateId();
        }
        public int is_Win(int[] map)
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
        public static int CreateId()
        {
            Random random = new Random();
            return random.Next(10000, 99999); 
        }
        async public void SendAll(Player winer)
        {
            await player_who_is_0.Send($"4|{winer.Client}");
            await player_who_is_X.Send($"4|{winer.Client}");
        }
        async public void SendAll(int[] map)
        {
            string movement = "";
            for (int i = 0; i < map.Length-1; i++)
            {
                movement += map[i].ToString();
                movement += ".";
            }
            movement += map[map.Length-1].ToString();
            await player_who_is_0.Send($"3|{movement}");
            await player_who_is_X.Send($"3|{movement}");
        }
        public bool SetX(int number)
        {
            if (number >= 0  && number <= 8 && map[number] == 0)
            {
                map[number] = 1;
                int winer = is_Win(map);
                if (winer == 1)
                {
                    Console.WriteLine("X win!");
                    SendAll(player_who_is_X);
                }
                else if(winer == 2)
                {
                    Console.WriteLine("0 win!");
                    SendAll(player_who_is_0);
                }
                SendAll(map);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Set0(int number)
        {
            if (number >= 0 && number <= 8 && map[number] == 0)
            {
                map[number] = 1;
                int winer = is_Win(map);
                if (winer == 1)
                {
                    Console.WriteLine("X win!");
                    SendAll(player_who_is_X);
                }
                else if (winer == 2)
                {
                    Console.WriteLine("0 win!");
                    SendAll(player_who_is_0);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
