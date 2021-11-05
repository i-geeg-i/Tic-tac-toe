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
        { get; set; }
        public Socket sock;
        public bool IsWeX = false;
        TcpClient client;
        NetworkStream stream;
        public int[] map = new int[9]; //creating map of the game (maybe it should be in another class)
        public Game(IPEndPoint addr)
        {
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //make socket for recive and send
            sock.ConnectAsync(addr);//connect addres and socket
            ConditionState = new StateOfMenu(this);
            client = new TcpClient("127.0.0.1", 1337);
            stream = client.GetStream();
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
            //Console.WriteLine(buffer.Length);
            await stream.WriteAsync(dataToSend, 0, dataToSend.Length);

        }
        public async Task<string> Recive() //function that recive data from server
        {
            byte[] buffer = new byte[4096];   //place for bytes that we will get after recive
            int totalReceivedLen = 0;  //variable that shows value of number recived part of data
            while (totalReceivedLen < 3)   //running until all part of data will be recived
            {
                int actuallyReceived = await stream.ReadAsync(buffer, totalReceivedLen, buffer.Length - totalReceivedLen);
                Console.WriteLine(Encoding.ASCII.GetString(buffer)); //debug
                totalReceivedLen += actuallyReceived;  //increasing the value of number recived data
            }
            int realLength = Convert.ToInt32(Encoding.ASCII.GetString(buffer));
            byte[] realBuffer = new byte[realLength];
            int totalReceived = 0;
            while (totalReceived < realLength)   //running until all data will be recive
            {
                int actuallyReceived = await stream.ReadAsync(realBuffer, totalReceivedLen, realBuffer.Length - totalReceivedLen);
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
