using BussinessLayer;
using BussinessLayer.BussinessModels;
using BussinessLayer.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Transactions;

namespace Receiver.Implementation
{
    public class PayloadImplementation
    {
        public static void AddHandle(byte[] transactionbytes)
        {
            StreamWriter writer;

            var transactionstring = Encoding.UTF8.GetString(transactionbytes);
            var transaction = JsonConvert.DeserializeObject<TransactionProtocol>(transactionstring);

            try
            {
                FileStream fileStream = new FileStream("./Transfers.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(transaction);
                }
                Console.WriteLine(transaction.Request_id);
                Console.WriteLine(transaction.Sender_id);
                Console.WriteLine(transaction.Timestamp);
                Console.WriteLine(transaction.Transaction);
                Console.WriteLine(transaction.Type_message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
            Console.WriteLine("Transaction " + transaction.Request_id + " was saved.");
        }

        internal string AddHandle(TransactionProtocol data, Settings settings)
        {
            string response;
            var datatrans = JsonConvert.DeserializeObject<TransactionData>(data.Transaction);
            if (string.IsNullOrWhiteSpace(data.Request_id) || string.IsNullOrWhiteSpace(data.Sender_id)
                || string.IsNullOrWhiteSpace(data.Transaction))
            {
                return "ADD | Error! Object is empty.";
            }else
            {
                Console.WriteLine("Add Handler");
                Console.WriteLine(data.Sender_id + " add to " + data.Request_id + " at " + data.Timestamp);
                Console.WriteLine("Owner: " + datatrans.Owner_card_id + "\nReceiver: " + datatrans.Recipient_card_id + "\nTransaction: " + datatrans.transactionType
                    + "\nCcy: " + datatrans.Ccy + "\nSum: " + datatrans.Transaction_summ);
                response = "ADD | Transaction type ADD with request " + data.Request_id + " and sender " + data.Sender_id + " at " + data.Timestamp;
                return response;
            }
        }

        internal string GiveHandle(TransactionProtocol data, Settings settings)
        {
            string response;
            var datatrans = JsonConvert.DeserializeObject<TransactionData>(data.Transaction);
            if (string.IsNullOrWhiteSpace(data.Request_id) || string.IsNullOrWhiteSpace(data.Sender_id)
                || string.IsNullOrWhiteSpace(data.Transaction))
            {
                return "GIVE | Error! Object is empty.";
            }
            else
            {
                Console.WriteLine("Add Handler");
                Console.WriteLine(data.Sender_id + " add to " + data.Request_id + " at " + data.Timestamp);
                Console.WriteLine("Owner: " + datatrans.Owner_card_id + "\nReceiver: " + datatrans.Recipient_card_id + "\nTransaction: " + datatrans.transactionType
                    + "\nCcy: " + datatrans.Ccy + "\nSum: " + datatrans.Transaction_summ);
                response = "GIVE | Transaction with request " + data.Request_id + " and sender " + data.Sender_id + " at " + data.Timestamp;
                return response;
            }
        }

        internal string ResponseHandle(TransactionProtocol data, Settings settings)
        {
            string response;
            var datatrans = JsonConvert.DeserializeObject<TransactionData>(data.Transaction);
            if (string.IsNullOrWhiteSpace(data.Request_id) || string.IsNullOrWhiteSpace(data.Sender_id)
                || string.IsNullOrWhiteSpace(data.Transaction))
            {
                return "RESPONSE | Error! Object is empty.";
            }
            else
            {
                Console.WriteLine("Add Handler");
                Console.WriteLine(data.Sender_id + " response to " + data.Request_id + " at " + data.Timestamp);
                Console.WriteLine("Owner: " + datatrans.Owner_card_id + "\nReceiver: " + datatrans.Recipient_card_id + "\nTransaction: " + datatrans.transactionType
                    + "\nCcy: " + datatrans.Ccy + "\nSum: " + datatrans.Transaction_summ);
                response = "RESPONSE | Transaction with request " + data.Request_id + " and sender " + data.Sender_id + " at " + data.Timestamp;
                return response;
            }
        }

    }
}
