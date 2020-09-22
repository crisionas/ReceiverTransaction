using BussinessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Receiver.Interfaces
{
    public interface IHandle
    {
        void Requests(Settings settings);
    }
}
