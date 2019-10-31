using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyNetQ;
using Microsoft.AspNetCore.Mvc;
using Web.MQ;

namespace Web.Controllers
{
    [Route("api")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private Sender _sender;
        private Receiver _receiver;
        private IBus _bus;

        public ValuesController(Sender sender, Receiver receiver, IBus bus)
        {
            _sender = sender;
            _receiver = receiver;
            _bus = bus;
        }

        [HttpGet("[controller]")]
        public ActionResult<string> Get()
        {
            //_bus.Publish(new Message { message = "test", sequence = 0});
            //_bus.Subscribe<Message>("0", _ => Console.WriteLine($"content : {_.message}, seq : {_.sequence}"));
            //_sender.Send("test");
            /*for (int i = 0; i < 5; i++)
            {
                Message m = _receiver.Receive();
                Console.WriteLine($"text : {m.message}, seq : {m.sequence}");
            }*/
            return "Hello";
        }

        [HttpGet("publish")]
        public string Publish(string message)
        {
            for (int i = 0; i < 5; i++)
            {
                _bus.Publish(new Message { message = message, sequence = i });
            }
            return "Ok";
        }

        [HttpGet("subscribe")]
        public string Subscribe()
        {
            string uid = Guid.NewGuid().ToString();
            string ret = "None";
            _bus.Subscribe<Message>(uid, _ => ret = $"content : {_.message}, seq : {_.sequence}");
            return ret;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
