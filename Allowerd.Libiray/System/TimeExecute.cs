using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Facepunch;

namespace Allowerd.Libiray.System
{
    public class TimeExecute : IDisposable
    {
        public static bool Enabled;

        public static bool EnableScopeCalls;

        public static Action<string> OnBegin;

        public static Action OnEnd;

        private Stopwatch stopwatch = new Stopwatch();

        private string warningName;

        private double warningMS;

        private int gcCount;

        private bool disposed;

        public static void BeginSample(string name) => TimeExecute.OnBegin?.Invoke(name);

        public static void EndSample() => TimeExecute.OnEnd?.Invoke();

        public static TimeExecute New(string name, int MaxMilisecond = 50)
        {
            TimeExecute timeWarning = Pool.Get<TimeExecute>();
            timeWarning.Start(name , MaxMilisecond);
            return timeWarning;
        }

        private void Start(string name, int MaxMilisecond = 50)
        {
            this.warningName = name;
            this.stopwatch.Reset();
            this.stopwatch.Start();
            this.gcCount = GC.CollectionCount(0);
            this.warningMS = MaxMilisecond;
            this.disposed = false;
            BeginSample(name);
        }
         
        void IDisposable.Dispose()
        {
            if (this.disposed)
            {
                return;
            }
            this.disposed = true;
            EndSample();
            bool flag = this.gcCount != GC.CollectionCount(0);
            if (stopwatch.Elapsed.TotalMilliseconds >= this.warningMS)
            {
                UnityEngine.Debug.LogErrorFormat("TimeExecuting: <{0}> took {1:0.00} ms ({2:0} ticks){3}", new object[]
                {
                    this.warningName,
                    this.stopwatch.Elapsed.TotalMilliseconds,
                    this.stopwatch.Elapsed.Ticks,
                    flag ? " [GARBAGE COLLECT]" : ""
                });
            }
            else
            {
                UnityEngine.Debug.LogWarningFormat("TimeExecuting: <{0}> took {1:0.00} ms ({2:0} ticks){3}", new object[]
                {
                    this.warningName,
                    this.stopwatch.Elapsed.TotalMilliseconds,
                    this.stopwatch.Elapsed.Ticks,
                    flag ? " [GARBAGE COLLECT]" : ""
                });
            }
            TimeExecute timeWarning = this;
            Pool.Free<TimeExecute>(ref timeWarning);
        }
    }
}
