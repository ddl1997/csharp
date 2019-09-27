using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpTest.FuncTest
{
    class AsyncTest
    {
        private static int value { get; set; } = 0;

        public static async Task TestAsync(bool isActive)
        {
            if (!isActive) return;

            _ = Increase();
            Console.WriteLine("1   " + value);
            Console.WriteLine("2   " + value);
            Console.WriteLine("3   " + value);

            await Increase();
            Console.WriteLine("4   " + value);
        }

        private static async Task Increase()
        {
            Thread.Sleep(1000);
            await Task.Run(() => value += 1);
        }
    }
}
