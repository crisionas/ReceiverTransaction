using BussinessLayer;
using Receiver;
using System;

namespace ReceiverSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            string ip="1";
            int port=900;
            Console.WriteLine("Receiver is ON!");
            var socket = new SocketReceiver();
            socket.Connect(ip, port);
            Console.WriteLine("Press any key...");
            Console.ReadLine();

        }
    }
}


