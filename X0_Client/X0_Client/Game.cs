using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace X0_Client
{
    class Game
    {
        public State _ConditionState
        { get; set; }
        public State ConditionState
        { get; set; }
        public Socket sock;
        public bool IsWeX = false;
        TcpClient client;
        NetworkStream stream;
        public int[] map = new int[9]; //creating map of the game (maybe it should be in another class)
        public Game()
        {
            ConditionState = new StateOfMenu(this);
            client = new TcpClient("127.0.0.1", 1337);
            stream = client.GetStream();
        }
        async public Task Send(string text)  //function that send data to server
        {
            byte[] bufferLen;  //place for bytes that we will get after encoding text
            byte[] dataToSendFromUser = Encoding.ASCII.GetBytes(text); //set value for buffer to send to server
            bufferLen = Encoding.ASCII.GetBytes(String.Format("{0:000}", dataToSendFromUser.Length));
            byte[] Result = new byte[bufferLen.Length + dataToSendFromUser.Length];
            bufferLen.CopyTo(Result, 0);
            dataToSendFromUser.CopyTo(Result, bufferLen.Length);
            await stream.WriteAsync(Result, 0, Result.Length);

        }
        async public Task<string> Recive() //function that recive data from server
        {
            byte[] buffer = new byte[4096];   //place for bytes that we will get after recive
            int totalReceivedLen = 0;  //variable that shows value of number recived part of data
            while (totalReceivedLen < 3)   //running until all part of data will be recived
            {
                int actuallyReceived = await stream.ReadAsync(buffer, totalReceivedLen, 3);
                Console.WriteLine(Encoding.ASCII.GetString(buffer)); //debug
                totalReceivedLen += actuallyReceived;  //increasing the value of number recived data
            }

            int realLength = Convert.ToInt32(Encoding.ASCII.GetString(buffer));
            byte[] realBuffer = new byte[realLength];
            int totalReceived = 0;
            while (totalReceived < realLength)   //running until all data will be recive
            {
                int actuallyReceived = await stream.ReadAsync(realBuffer, totalReceived, realBuffer.Length - totalReceived);
                Console.WriteLine(Encoding.ASCII.GetString(realBuffer)); //debug
                totalReceived += actuallyReceived;  //increasing the value of number recived data
            }
            return Encoding.ASCII.GetString(realBuffer);
        }
        void winResponse(int whoWin, bool x)
        {
            if ((x && whoWin == 1) || (!x && whoWin == 2))
            {
                Console.WriteLine("You win!");
            }
            else
            {
                Console.WriteLine("You lose!");
            }
        }   
        public async Task Run()
        {
            while (true)
            {
                await ConditionState.Handle();
            }
        }
    }
}
