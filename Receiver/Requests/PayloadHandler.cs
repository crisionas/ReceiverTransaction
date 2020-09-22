using BussinessLayer;
using BussinessLayer.BussinessModels;
using BussinessLayer.Enums;
using Newtonsoft.Json;
using Receiver.Implementation;
using Receiver.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Receiver.Route
{
    public class PayloadHandler : PayloadImplementation, IHandle
    {
        void IHandle.Requests(Settings settings)
        {
            var request = Convert.ToBase64String(settings.Buffer);
            TransactionProtocol transaction = JsonConvert.DeserializeObject<TransactionProtocol>(request);
            if (transaction.Type_message == MessageType.add)
            {
                AddHandle(transaction, settings);
            }
            else if (transaction.Type_message == MessageType.give)
            {
                GiveHandle(transaction, settings);
            }
            else if (transaction.Type_message == MessageType.response)
            {
                ResponseHandle(transaction, settings);
            }

        }
    }
}
