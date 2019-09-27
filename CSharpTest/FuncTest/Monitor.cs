using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpTest.FuncTest
{
    public class Monitor
    {
        private int value { get; set; }
        private Func<int, string> monitor { get; set; }

        public Monitor(Func<int, string> monitor)
        {
            value = 0;
            this.monitor = monitor;
        }

        public void Increse()
        {
            value++;
            monitor(value);
        }

        public void Random()
        {
            value = new Random((int)DateTime.Now.Ticks).Next(100);
            monitor(value);
        }
    }

    public class MonitorAction
    {
        private int value { get; set; }
        public Func<int, string> func { get; set; }

        public MonitorAction()
        {
            value = 100;
            func = _ => 
            {
                value = _;
                return "OK";  
            };
        }

        public void Print()
        {
            Console.WriteLine(value);
        }
    }
}
