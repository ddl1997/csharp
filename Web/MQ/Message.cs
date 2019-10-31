using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.MQ
{
    public class Message
    {
        public string message { get; set; }
        public int sequence { get; set; }
    }
}
