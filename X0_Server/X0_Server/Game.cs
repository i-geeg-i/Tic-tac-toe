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
        public Player player_who_is_O { get; set; } //y = 2
        public List<Player> players = new List<Player>();
        private int[] map = new int[9];
        public int id { get; }
        public Game(Player FirstPlayer)
        {
            players.Add(FirstPlayer);
        }
        public int is_Win()
        {
            if (number >= 0 && number <= 8 && map[number] == 0)
            {

            }
                return 0;
        }
        public bool SetX(int number)
        {
            if (number >= 0  && number <= 8 && map[number] == 0)
            {
                map[number] = 1;
                if (map[0] == 1 && map[1] == 1 && map[2] == 1)
                {
                    Console.WriteLine("Win!");
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SetO(int number)
        {
            if (number >= 0 && number <= 8 && map[number] == 0)
            {
                map[number] = 1;
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
