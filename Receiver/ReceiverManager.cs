
using Receiver.Interfaces;
using Receiver.Route;
using System;
using System.Collections.Generic;
using System.Text;

namespace Receiver
{
    public class ReceiverManager
    {
        public IHandle GetRoute()
        {
            return new PayloadHandler();
        }
    }
}
