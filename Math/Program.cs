using ExcelLibrary.SpreadSheet;
using MathFunc.MathBased;
using MathFunc.RandomFunc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace MathFunc
{
    class Program
    {
        static void Main(string[] args)
        {

            RandomTest1(false);
            RandomTest2(false);
            //GroupByTest(false);
            PrizePoolTest(false);
            JsonConvertTest(false);
            FinallyTest(true);


            //Console.WriteLine(Math.Round(1.5));
        }

        public static void RandomTest1(bool isActive)
        {
            if (!isActive) return;
            RandomInt ran = new RandomInt();
            List<int> l = ran.GetRandomList(20, 100, 0);
            if (l == null)
            {
                Console.WriteLine("参数异常！");
                return;
            }
            l.ForEach(_ => Console.WriteLine(_));
        }

        public static void RandomTest2(bool isActive)
        {
            if (!isActive) return;
            RandomNormal ran = new RandomNormal();
            List<int> l = ran.IntegerTruncatedNormalRandomList(50, 200, 0, 80, 2);
            if (l == null)
            {
                Console.WriteLine("参数异常！");
                return;
            }
            l.ForEach(_ => Console.WriteLine(_));
        }

        /*public static void GroupByTest(bool isActive)
        {
            if (!isActive) return;
            Dictionary<int, A> l = new Dictionary<int, A>()
            {
                { 1, new A {a = 1, b = 1, c = 1}},
                { 2, new A {a = 1, b = 1, c = 1}},
                { 3, new A {a = 1, b = 1, c = 1}},
                { 4, new A {a = 2, b = 1, c = 1}},
                { 5, new A {a = 2, b = 1, c = 1}},
                { 6, new A {a = 3, b = 1, c = 1}}
            };
            var list = l.Select(_ => _.Value).GroupBy(_ => _.a).Select(_ => new
            {
                Id = _.Key,
                A = _.GetEnumerator(),
                Amount = _.Count()
            });
            var newList = list.Select(_ =>
            {
                _.A.MoveNext();
                A a = _.A.Current;
                a.a = a.a == _.Id ? a.a : _.Id;
                a.c = _.Amount;
                return a;
            }).ToList();
            newList.ForEach(_ => Console.WriteLine("a : {0} b : {1} c : {2}", _.a, _.b, _.c));
        }*/

        public static void PrizePoolTest(bool isActive)
        {
            if (!isActive) return;
            int size = 10000;
            List<Prize> prizes = new List<Prize>()
            {
                new Prize{ Id = "1", Amount = 10},
                new Prize{ Id = "2", Amount = 40},
                new Prize{ Id = "3", Amount = 100},
                new Prize{ Id = "4", Amount = 250}
            };
            PrizePool pool = new PrizePool();
            pool.Shuffle(prizes, size);
            int prizeCount1 = 0;
            int prizeCount2 = 0;
            int prizeCount3 = 0;
            int prizeCount4 = 0;
            Workbook workbook = new Workbook();
            Worksheet worksheet = new Worksheet("Sheet");

            for (int i = 0; i < size; i++)
            {
                Prize p = pool.Draw();
                if (p != null)
                {
                    switch(p.Id)
                    {
                        case "1": prizeCount1++; break;
                        case "2": prizeCount2++; break;
                        case "3": prizeCount3++; break;
                        case "4": prizeCount4++; break;
                    }
                    //Console.WriteLine("Congratulation! Prize {0}! Total count {1}.", p.Id, i);
                }
                /*if (i > 0 && (i % 200 == 0 || i + 1 == 1000))
                {
                    Console.WriteLine("Current Prize 1 Probability : {0}.", Convert.ToDouble(prizeCount1) / Convert.ToDouble(i + 1));
                    Console.WriteLine("Current Prize 2 Probability : {0}.", Convert.ToDouble(prizeCount2) / Convert.ToDouble(i + 1));
                    Console.WriteLine("Current Prize 3 Probability : {0}.", Convert.ToDouble(prizeCount3) / Convert.ToDouble(i + 1));
                    Console.WriteLine("Current Prize 4 Probability : {0}.", Convert.ToDouble(prizeCount4) / Convert.ToDouble(i + 1));
                }*/

                /*
                worksheet.Cells[i, 0] = new Cell(i);
                worksheet.Cells[i, 1] = new Cell(Convert.ToDouble(prizeCount1) / Convert.ToDouble(i + 1));
                worksheet.Cells[i, 2] = new Cell(Convert.ToDouble(prizeCount2) / Convert.ToDouble(i + 1));
                worksheet.Cells[i, 3] = new Cell(Convert.ToDouble(prizeCount3) / Convert.ToDouble(i + 1));
                worksheet.Cells[i, 4] = new Cell(Convert.ToDouble(prizeCount4) / Convert.ToDouble(i + 1));
                */
                worksheet.Cells[i, 0] = new Cell(i);
                worksheet.Cells[i, 1] = new Cell(p == null ? 0: Convert.ToInt32(p.Id));

            }
            workbook.Worksheets.Add(worksheet);
            workbook.Save("test6.xls");
            Console.WriteLine("Finished.");
        }

        public static void JsonConvertTest(bool isActive)
        {
            if (!isActive) return;
            B b = new B();
            var jsonSerializer = new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            Console.WriteLine(JsonConvert.SerializeObject(b, jsonSerializer));
        }

        public static void FinallyTest(bool isActive)
        {
            if (!isActive) return;
            try {
                return;
            }
            finally
            {
                Console.WriteLine("OK");
            }
        }

        public double Average(int[] arr)
        {
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }
            return Convert.ToDouble(sum) / arr.Length;
        }

        public double StandardDeviation(int[] arr)
        {
            double avg = Average(arr);
            double sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += Math.Pow((arr[i] - avg), 2.0);
            }
            return sum / arr.Length;
        }
    }

    public class A
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public string Detail { get; set; }
        public string Notice { get; set; }
        [Range(0, 10000)]
        public int Rate { get; set; }
        public int Amount { get; set; }

        public A()
        {
            Id = "a";
            Name = "b";
            Remark = "c";
            Detail = "d";
            Notice = "e";
            Rate = 100;
            Amount = 200;
        }
    }

    [Serializable]
    public class B
    {
        [JsonProperty]
        private ConcurrentDictionary<int, A> a { get; set; }
        public int b { get; set; }
        public int c { get; set; }

        public B()
        {
            a = new ConcurrentDictionary<int, A>();
            for (int i = 0; i < 20; i++)
            {
                a.TryAdd(i, new A ());
            }
            b = 3;
            c = 20;
        }
    }
}
