using System;
using System.Collections.Generic;
using System.Linq;

namespace MathFunc.RandomFunc
{
    public class RandomInt
    {
        private long seed { get; set; }

        public RandomInt()
        {
            seed = DateTime.Now.Ticks;
        }

        public RandomInt(long seed)
        {
            this.seed = seed;
        }

        /// <summary>
        /// 返回互不相同的随机排列整型数字序列，序列大小为size，每个数字大于等于min，小于max
        /// </summary>
        /// <param name="size">列表大小</param>
        /// <param name="max">最大值（无法取到）</param>
        /// <param name="min">最小值</param>
        /// <returns></returns>
        public List<int> GetRandomList(int size, int max, int min)
        {
            if (size > max - min)
                return null;
            List<int> result = new List<int>();
            int[] intArr = GetIntArr(max, min).ToArray();
            int last = max - min;
            Random random = new Random((int)seed);
            for (int i = 0; i < size; i++)
            {
                int index = random.Next(last);
                result.Add(intArr[index]);
                last--;
                int temp = intArr[last];
                intArr[last] = intArr[index];
                intArr[index] = temp;
            }

            return result;
        }

        
        public List<int> GetTruncatedNormalRandomList(int size, int max, int min)
        {
            if (size > max - min)
                return null;
            List<int> result = new List<int>();
            int[] intArr = GetIntArr(max, min).ToArray();
            int last = max - min;
            Random random = new Random((int)seed);
            for (int i = 0; i < size; i++)
            {
                int index = random.Next(last);
                result.Add(intArr[index]);
                last--;
                int temp = intArr[last];
                intArr[last] = intArr[index];
                intArr[index] = temp;
            }

            return result;
        }

        /// <summary>
        /// 返回大于等于min，小于max的递增整型数字序列，序列大小为max-min
        /// </summary>
        /// <param name="max">最大值（无法取到）</param>
        /// <param name="min">最小值</param>
        /// <returns></returns>
        private IEnumerable<int> GetIntArr(int max, int min)
        {
            for (int i = min; i < max; i++)
                yield return i;
        }
    }
}
