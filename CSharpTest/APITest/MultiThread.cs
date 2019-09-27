
using CSharpTest.FuncTest.Utils;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace CSharpTest.APITest
{
    class MultiThread
    {
        
        public static void Request(bool isActive)
        {
            if (!isActive) return;

            string url = "http://localhost:5000/Oauth/wx64d1b099d6beceb5/lottery/draw/11";
            Network network = new Network();
            int threads_size = 50;
            Thread[] t_arr = new Thread[threads_size];
            HttpWebRequest settings(HttpWebRequest _)
            {
                _.Headers.Add("Cookie", "wx_wx64d1b099d6beceb5=" + Guid.NewGuid());
                return _;
            }
            for (int i = 0; i < threads_size; i++)
            {
                t_arr[i] = new Thread(() => network.Post(url, "", settings));
            }

            for (int i = 0; i < threads_size; i++)
            {
                t_arr[i].Start();
            }
        }

        public static void LockTest(bool isActive)
        {
            if (!isActive) return;

            DistributedLock _lock = new DistributedLock();
            int threads_size = 100;
            Thread[] t_arr = new Thread[threads_size];

            for (int i = 0; i < threads_size; i++)
            {
                t_arr[i] = new Thread(_ =>
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    string value = Guid.NewGuid().ToString();
                    var result = _lock.TryLockScript("test", value);
                    sw.Stop();
                    Console.WriteLine(sw.ElapsedMilliseconds);
                });
            }

            for (int i = 0; i < threads_size; i++)
            {
                t_arr[i].Start();
            }
        }

        public static void LockAndUnlock(bool isActive)
        {
            if (!isActive) return;

            DistributedLock _lock = new DistributedLock();

            string value1 = Guid.NewGuid().ToString();
            var result = _lock.TryLockTransaction("test", value1);
            Console.WriteLine("Thread {0} output: {1}.", value1, result);
            Thread.Sleep(1000);

            string value2 = Guid.NewGuid().ToString();
            result = _lock.TryLockTransaction("test", value2);
            Console.WriteLine("Thread {0} output: {1}.", value2, result);

            _lock.UnLock("test");

            string value3 = Guid.NewGuid().ToString();
            result = _lock.TryLockTransaction("test", value3);
            Console.WriteLine("Thread {0} output: {1}.", value3, result);
        }
    }
}
