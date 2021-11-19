using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class KnowledgeCenter
    {
        private static KnowledgeCenter instance;
        public readonly string codeOfGameID = "1";
        public readonly string codeOfListOfGames = "2";
        public readonly string codeOfMovement = "3";
        public readonly string codeOfWin = "4";
        public readonly string codeOfGameStart = "5";
        public readonly string codeOfNewUser = "6";
        public readonly string codeOfDraw = "7";
        private KnowledgeCenter()
        { }
        public static KnowledgeCenter getInstance()
        {
            if (instance == null)
                instance = new KnowledgeCenter();
            return instance;
        }
        
    }
}
