using BussinessLayer.BussinessModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace BussinessLayer
{
    public class PayloadHandler
    {
        public static void Handle(byte[] transactionbytes,Settings settings)
        {

            var transactionstring = Encoding.UTF8.GetString(transactionbytes);
            var data = JsonConvert.DeserializeObject<TransactionProtocol>(transactionstring);
            var transaction = JsonConvert.DeserializeObject<TransactionData>(data.Transaction);
            //if (string.IsNullOrWhiteSpace(data.Request_id) || string.IsNullOrWhiteSpace(data.Sender_id)
            //    || string.IsNullOrWhiteSpace(data.Transaction))
            //{
            //    data.Type_message = Enums.MessageType.error;
            //    data.Transaction = null;
            //    data.Message = $"Error! Transaction from {data.Sender_id} to {data.Request_id} at {data.Timestamp} was unsuccessful.";
            //    var byte_message = ConvertToBytes(data);
            //    settings.Socket.Send(byte_message);
            //}
            //else
            //{
                //data.Type_message = Enums.MessageType.response;
                //data.Transaction = null;
                //data.Message = $"Successful! Transaction from {data.Sender_id} to {data.Request_id} at {data.Timestamp} was successful.";
                //data.Timestamp = DateTime.Now;
                //var byte_message = ConvertToBytes(data);

                Console.WriteLine("\n\n" + data.Sender_id + " add to " + data.Request_id + " at " + data.Timestamp);

                Console.WriteLine("Owner: " + transaction.Owner_card_id + "\nReceiver: " + transaction.Recipient_card_id + "\nTransaction: " + transaction.transactionType
                    + "\nCcy: " + transaction.Ccy + "\nSum: " + transaction.Transaction_summ);
                //settings.Socket.Send(byte_message);
           // }    
        }

        //private static byte[] ConvertToBytes(TransactionProtocol message)
        //{
        //    var transact_format = JsonConvert.SerializeObject(message);
        //    var byte_array = UTF8Encoding.UTF8.GetBytes(transact_format);
        //    return byte_array;
        //}

    }
}
