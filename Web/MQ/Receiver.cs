using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.MQ
{
    public class Receiver
    {
        private readonly IBus _bus;

        public Receiver(IBus bus)
        {
            _bus = bus;
        }

        public Message Receive()
        {
            Message m = null;
            _bus.Receive<Message>("test", _ => m = _);
            return m;
        }
    }
}
