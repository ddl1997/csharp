using EasyNetQ.AutoSubscribe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.MQ
{
    public class Consumer : IConsume<Message>
    {
        public void Consume(Message message)
        {
            Console.WriteLine($"text : {message.message}, seq : {message.sequence}");
        }
    }
}
