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
        public State _ConditionState;
        public State ConditionState
        {
            get { return _ConditionState; }
            set { _ConditionState = value; }
        }
        public static Socket sock = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,
            ProtocolType.Tcp
            ); //make socket for recive and send
        public bool IsWeX = false;
        public int[] map = new int[9]; //creating map of the game (maybe it should be in another class)
        public Game(IPEndPoint addr)
        {
            sock.Connect(addr);//connect addres and socket
            ConditionState = new StateOfMenu(this);
        }
        public async Task Send(string text)  //function that send data to server
        {
            byte[] buffer;
            byte[] bufferText;
            byte[] dataToSend = Encoding.ASCII.GetBytes(text); //set value for buffer to send to server
            bufferText = Encoding.ASCII.GetBytes(String.Format("{0:000}", dataToSend.Length));
            buffer = new byte[bufferText.Length + dataToSend.Length];
            bufferText.CopyTo(buffer, 0);
            dataToSend.CopyTo(buffer, bufferText.Length);
            int totalSent = 0;  //variable that shows value of number sent data
            Console.WriteLine(buffer.Length);
            while (totalSent < buffer.Length)   //sending until we have sent all data 
            {
                int actuallySent = sock.Send(
                buffer,
                totalSent,
                buffer.Length - totalSent,
                SocketFlags.None
                );  //sending data
                totalSent += actuallySent;  //increasing the value of number sent data
            }
        }
        public async Task<string> Recive() //function that recive data from server
        {
            byte[] buffer = new byte[4096];   //place for bytes that we will get after recive
            int totalReceivedLen = 0;  //variable that shows value of number recived part of data
            while (totalReceivedLen < 3)   //running until all part of data will be recived
            {
                int actuallyReceived = sock.Receive(
                buffer,
                totalReceivedLen,
                buffer.Length - totalReceivedLen,
                SocketFlags.None
                );  //reciving data
                //error there
                Console.WriteLine(Encoding.ASCII.GetString(buffer)); //debug
                totalReceivedLen += actuallyReceived;  //increasing the value of number recived data
            }
            int realLength = Convert.ToInt32(Encoding.ASCII.GetString(buffer));
            byte[] realBuffer = new byte[realLength];
            int totalReceived = 0;
            while (totalReceived < realLength)   //running until all data will be recive
            {
                int actuallyReceived = sock.Receive(
                realBuffer,
                totalReceivedLen,
                realBuffer.Length - totalReceivedLen,
                SocketFlags.None
                );  //reciving data
                //error there
                Console.WriteLine(Encoding.ASCII.GetString(realBuffer)); //debug
                totalReceivedLen += actuallyReceived;  //increasing the value of number recived data
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
        public void Run()
        {
            while (true)
            {
                ConditionState.Handle();
            }
        }
    }
}
