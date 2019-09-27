using System;
using System.Collections.Generic;
using System.Linq;

namespace MathFunc.RandomFunc
{
    class RandomNormal
    {
        public double NormalNext()
        {
            double u1 = new Random((int)DateTime.Now.Ticks).NextDouble();
            double u2 = new Random((int)(DateTime.Now.Ticks * 0.5)).NextDouble();
            //sqrt(-2 * log(u1)) * cos(2 * pi * u2)
            double normalDouble = Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
            return normalDouble;
        }

        public double NormalNext(double avg, double stddv)
        {
            return NormalNext() * stddv + avg;
        }

        public List<double> NormalRandomList(int size, double avg, double stddv)
        {
            HashSet<double> t = new HashSet<double>();
            List<double> l = new List<double>();
            int count = 0;

            while (count < size)
            {
                double ran = NormalNext(avg, stddv);
                if (!t.Contains(ran))
                {
                    t.Add(ran);
                    l.Add(ran);
                    count++;
                }
            }

            return l;
        }

        public List<int> IntegerNormalRandomList(int size, double avg, double stddv)
        {
            HashSet<int> t = new HashSet<int>();
            List<int> l = new List<int>();
            int count = 0;

            while (count < size)
            {
                int ran = (int)Math.Round(NormalNext(avg, stddv));
                if (!t.Contains(ran))
                {
                    t.Add(ran);
                    l.Add(ran);
                    count++;
                }
            }

            return l;
        }

        /// <summary>
        /// 返回互不相同的随机排列浮点型数字序列，序列服从截断的正态分布，序列大小为size，每个数字大于等于min，小于max
        /// </summary>
        /// <param name="size">列表大小</param>
        /// <param name="max">最大值（无法取到）</param>
        /// <param name="min">最小值</param>
        /// <param name="avg">均值</param>
        /// <param name="min">标准差</param>
        /// <returns></returns>
        public List<double> TruncatedNormalRandomList(int size, int max, int min, double avg, double stddv)
        {
            if (size > max - min)
                return null;
            HashSet<double> t = new HashSet<double>();
            List<double> l = new List<double>();
            int count = 0;

            while (count < size)
            {
                double ran = NormalNext(avg, stddv);
                if (!t.Contains(ran) && ran >= min && ran < max)
                {
                    t.Add(ran);
                    l.Add(ran);
                    count++;
                }
            }

            return l;
        }

        public List<int> IntegerTruncatedNormalRandomList(int size, int max, int min, double avg, double stddv)
        {
            if (size > max - min)
                return null;
            HashSet<int> t = new HashSet<int>();
            List<int> l = new List<int>();
            int count = 0;

            while (count < size)
            {
                int ran = (int)Math.Round(NormalNext(avg, stddv));
                if (!t.Contains(ran) && ran >= min && ran < max)
                {
                    t.Add(ran);
                    l.Add(ran);
                    count++;
                }
                Console.WriteLine("Current count : {0}", count);
            }

            return l;
        }
    }
}
