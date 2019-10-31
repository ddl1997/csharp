using CSharpTest.APITest;
using CSharpTest.FuncTest;
using CSharpTest.FuncTest.Utils;
using CSharpTest.Utils;
using CSharpTest.Utils.StringOperation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CSharpTest
{
    class Program
    {
        static DistributedLock _lock = new DistributedLock();
        static void Main(string[] args)
        {
            //DistributedLock.Config("localhost","wechat");
            /*MonitorAction ma = new MonitorAction();
            Monitor m = new Monitor(ma.func);
            m.Increse();
            ma.Print();*/

            /*for (int i = 0; i < 10; i++)
            {
                NetTest();
            }*/
            MultiThread.Request(false);
            MultiThread.LockTest(false);
            MultiThread.LockAndUnlock(false);

            /*Console.WriteLine(StringTransform.StylePrice(12233.444));
            Console.WriteLine(StringTransform.DigitalToChinese(101010101));
            Console.WriteLine("a" + 1 + 2);*/

            /*var html = File.Open("/Temp/temp.html", FileMode.Open);
            var buf = new byte[html.Length];
            html.Read(buf, 0, buf.Length);
            string h = Encoding.UTF8.GetString(buf);
            //string temp = @"<html><h>aaaaaa</h>\n<p>bbbbbb</p></html>";
            PdfHelper.HtmlToPdf(h);*/
            //html.Close();

            List<A> list = new List<A>
            {
                new A { a = "a", b = "a" },
                new A { a = "b", b = "b" },
                new A { a = "c", b = "c" }
            };

            var item = list[1];
            list.ForEach(_ => {
                if (_.a == "b")
                    list.Remove(_);
            });
            list.ForEach(_ => Console.WriteLine($"{_.a} {_.b}"));

            var mi = typeof(List<string>).GetMethod("ToString");

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

    public class A
    {
        public string a { get; set; }
        public string b { get; set; }
    }

}
