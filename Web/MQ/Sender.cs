using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.MQ
{
    public class Sender
    {
        private readonly IBus _bus;

        public Sender(IBus bus)
        {
            _bus = bus;
        }

        public void Send(string message)
        {
            for (int i = 0; i < 5; i++)
            {
                _bus.Send<Message>("test", new Message { message = message, sequence = i });
            }
            
        }
    }
}
