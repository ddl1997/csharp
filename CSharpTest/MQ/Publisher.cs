using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpTest.MQ
{
    public class Publisher
    {
        private readonly IBus _bus;

        public Publisher(IBus bus)
        {
            _bus = bus;
        }
    }
}
