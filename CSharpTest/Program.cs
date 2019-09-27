using CSharpTest.APITest;
using CSharpTest.FuncTest;
using CSharpTest.FuncTest.Utils;
using System;
using System.Net;

namespace CSharpTest
{
    class Program
    {
        static DistributedLock _lock = new DistributedLock();
        static void Main(string[] args)
        {
            DistributedLock.Config("localhost","wechat");
            /*MonitorAction ma = new MonitorAction();
            Monitor m = new Monitor(ma.func);
            m.Increse();
            ma.Print();*/

            /*for (int i = 0; i < 10; i++)
            {
                NetTest();
            }*/
            MultiThread.Request(true);
            MultiThread.LockTest(false);
            MultiThread.LockAndUnlock(false);

            //Console.WriteLine(_lock.ScriptTest());

            //AsyncTest.TestAsync(true);

            //Console.WriteLine("Finished!");
        }

        public static void NetTest()
        {
            string url = "http://localhost:5000/Oauth/wx64d1b099d6beceb5/lottery/draw/12";
            Network network = new Network();
            Func<HttpWebRequest, HttpWebRequest> settings = _ =>
            {
                _.Headers.Add("Cookie", "wx_wx64d1b099d6beceb5=" + Guid.NewGuid());
                return _;
            };
            network.Post(url, "", settings);
        }
    }
}
