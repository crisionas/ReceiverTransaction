using BussinessLayer;
using BussinessLayer.BussinessModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TransactionAPI
{
    public class ReceiverSocket
    {
        protected const int SIZE_RECEIVE_BUFFER = 1024;
        private Socket _socket;
        public ReceiverSocket()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Connect(string IP, int Port)
        {
            _socket.BeginConnect(new IPEndPoint(IPAddress.Parse(IP), Port), ConnectedCallback, null);
            Console.WriteLine("Waiting for connection...");
        }

        private void ConnectedCallback(IAsyncResult ar)
        {
            if (_socket.Connected)
            {
                Console.WriteLine("Receiver is connected to Queue Server...");
                StartReceive();
            }
            else
            {
                Console.WriteLine("Error! Can't connect to Queue Server... " + _socket.Connected);

            }
        }

        private void StartReceive()
        {
            byte[] ReceiveBuffer = new byte[SIZE_RECEIVE_BUFFER];
            TransactionProtocol transaction = new TransactionProtocol();
            transaction.Socket = _socket;
            _socket.BeginReceive(ReceiveBuffer, 0, transaction.Buffer.Length, SocketFlags.None, ReceiveCallBack, transaction);
        }

        private void ReceiveCallBack(IAsyncResult ar)
        {
            TransactionProtocol transaction = ar.AsyncState as TransactionProtocol;
            try
            {
                SocketError response;
                int buffsize = _socket.EndReceive(ar, out response);
                byte[] payloadbytes = new byte[buffsize];
                Array.Copy(transaction.Buffer, payloadbytes, payloadbytes.Length);
                PayloadHandler.Handle(payloadbytes);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Can't receive data from Queue Server {e.Message}");
            }
            finally
            {
                try
                {
                    transaction.Socket.BeginReceive(transaction.Buffer, 0, transaction.Buffer.Length, SocketFlags.None, ReceiveCallBack, transaction);
                }
                catch(Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                    transaction.Socket.Close();
                }
            }
        }
    }
}
