using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Server
{
    class KnowledgeCenter
    {
        private static KnowledgeCenter instance;
        public List<Game> games = new List<Game>();
        public List<Player> players = new List<Player>();
        private KnowledgeCenter()
        { }

        public static KnowledgeCenter getInstance()
        {
            if (instance == null)
                instance = new KnowledgeCenter();
            return instance;
        }
        public Game FindGame(int id)
        {
            for (int i = 0; i < games.Count; i++)
            {
                if(games[i].id == id)
                {
                    return games[i];
                }
            }
            return new Game(new Player(new System.Net.Sockets.TcpClient()));
        }
        /*public List<Game> GetOpenGames()
        {
            List<Game> toReturn = new List<Game>();
            for (int i = 0; i < games.Count; i++)
            {
                if (!games[i].started && games[i].player_who_is_X == null && games[i].player_who_is_0 == null)
                {
                    toReturn.Add(games[i]);
                }
            }
            return toReturn;
        }*/
        public string GetOpenGames()
        {
            string toReturn = "";
            for (int i = 0; i < games.Count; i++)
            {
                if (!games[i].started && games[i].player_who_is_X == null && games[i].player_who_is_0 == null)
                {
                    toReturn += games[i].id + ".";
                }
            }
            return toReturn;
        }

    }
}
