using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace X0_Server
{
    class KnowledgeCenter
    {
        private static KnowledgeCenter instance;
        public List<Game> games = new List<Game>();
        public Dictionary<TcpClient,Player> players = new Dictionary<TcpClient, Player>();
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
        public string GetOpenGames()
        {
            string toReturn = "-1";
            for (int i = 0; i < games.Count; i++)
            {
                if (!games[i].started && games[i].player_who_is_X == null && games[i].player_who_is_0 == null)
                {
                    if(i != games.Count - 1)
                    {
                        toReturn += games[i].id + ".";
                    }
                    else
                    {
                        toReturn += games[i].id;
                    }
                }
            }
            return toReturn;
        }

    }
}
