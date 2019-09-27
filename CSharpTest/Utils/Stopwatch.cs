using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpTest.Utils
{
    public class Stopwatch
    {
        private System.Diagnostics.Stopwatch sw { get; set; }
        private long totalTime { get; set; }
        private List<long> partTime { get; set; }

        public Stopwatch()
        {
            sw = new System.Diagnostics.Stopwatch();
            totalTime = 0;
            partTime = new List<long>();
        }

        public void Start() => sw.Start();

        public void Stop()
        {
            sw.Stop();
            totalTime += sw.ElapsedMilliseconds;
            partTime.Add(sw.ElapsedMilliseconds);
        }

        public void Record()
        {
            Stop();
            sw.Restart();
        }

        public void Reset()
        {
            sw.Reset();
            totalTime = 0;
            partTime.Clear();
        }

        public long GetTotalTime() => totalTime;

        public List<long> GetPartTime() => partTime;

    }
}
