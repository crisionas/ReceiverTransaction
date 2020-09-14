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
        public static void Handle(byte[] transactionbytes)
        {
            FileStream fileStream;
            StreamWriter writer;

            var transactionstring = Encoding.UTF8.GetString(transactionbytes);
            var transaction = JsonConvert.DeserializeObject<TransactionProtocol>(transactionstring);

            try
            {
                fileStream = new FileStream("./Transfers.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine(transaction);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }
            Console.WriteLine("Transaction " + transaction.Request_id + " was saved.");
        }
    }
}
