using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathFunc.MathBased
{
    public class PrizePool
    {
        private ConcurrentDictionary<int, Prize> indexes { get; set; }
        private int _size { get; set; }
        private int firstIndex { get; set; }

        public PrizePool()
        {
            indexes = new ConcurrentDictionary<int, Prize>();
            _size = 0;
            firstIndex = 0;
        }

        public Prize Draw()
        {
            if (firstIndex == _size)
                return null;
            if (indexes.ContainsKey(firstIndex))
            {
                if (indexes.TryGetValue(firstIndex, out Prize p))
                {
                    firstIndex++;
                    return p;
                }
            }
            firstIndex++;
            return null;
        }

        public bool Shuffle(List<Prize> prizes, int size)
        {
            if (prizes == null)
            {
                _size = size;
                var ps = indexes.Select(_ => _.Value).ToList();
                int remain = ps.Count();
                var pIndexes = GetRandomList(remain, size, 0);
                for (int i = 0; i < remain; i++)
                {
                    indexes.TryAdd(pIndexes[i], ps[i]);
                }
                firstIndex = 0;
                return true;
            }

            int sum = prizes.Sum(_ => _.Amount);
            if (sum > size)
                return false;
            _size = size;
            var sums = prizes.Select(_ => _.Amount);
            var prizeIndexes = GetRandomList(sum, size, 0);
            int count = 0;
            foreach (Prize p in prizes)
            {
                for (int i = 0; i < p.Amount; i++)
                {
                    indexes.TryAdd(prizeIndexes[count], new Prize
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Remark = p.Remark,
                        Notice = p.Notice,
                        Amount = 1
                    });
                    count++;
                }
            }
            firstIndex = 0;
            return true;
        }

        public void Clean()
        {
            indexes = new ConcurrentDictionary<int, Prize>();
            _size = 0;
            firstIndex = 0;
        }

        public List<Prize> GetPrizes()
        {
            var amounts = indexes.Select(_ => _.Value).GroupBy(_ => _.Id).Select(_ => new
            {
                Id = _.Key,
                Prize = _.GetEnumerator(),
                Amount = _.Count()
            });
            var prizes = amounts.Select(_ =>
            {
                _.Prize.MoveNext();
                Prize p = _.Prize.Current;
                p.Id = p.Id == _.Id ? p.Id : _.Id;
                p.Amount = _.Amount;
                return p;
            });

            return prizes.ToList();
        }

        /// <summary>
        /// 返回互不相同的随机排列整型数字序列，序列大小为size，每个数字大于等于min，小于max
        /// </summary>
        /// <param name="size">列表大小</param>
        /// <param name="max">最大值（无法取到）</param>
        /// <param name="min">最小值</param>
        /// <returns></returns>
        private List<int> GetRandomList(int size, int max, int min)
        {
            if (size > max - min)
                return null;
            List<int> result = new List<int>();
            int[] intArr = GetIntArr(max, min).ToArray();
            int last = max - min;
            Random random = new Random((int)DateTime.Now.Ticks);
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

    public class Prize
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public string Notice { get; set; }
        public int Amount { get; set; }
    }
}
