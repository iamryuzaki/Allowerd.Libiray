using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Facepunch;

namespace Allowerd.Libiray.System
{
    public class TimeWarning : IDisposable
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

        public static void BeginSample(string name)
        {
            if (TimeWarning.OnBegin != null)
            {
                TimeWarning.OnBegin(name);
            }
        }

        public static void EndSample()
        {
            if (TimeWarning.OnEnd != null)
            {
                TimeWarning.OnEnd();
            }
        }

        public static TimeWarning New(string name, float MaxTime = 0f)
        {
            TimeWarning timeWarning = Pool.Get<TimeWarning>();
            timeWarning.Start(name);
            return timeWarning;
        }

        private void Start(string name, float MaxTime = 0f)
        {
            this.warningName = name;
            this.stopwatch.Reset();
            this.stopwatch.Start();
            this.gcCount = GC.CollectionCount(0);

            this.disposed = false;
            if (TimeWarning.OnBegin != null)
            {
                TimeWarning.OnBegin(name);
            }
        }
         
        void IDisposable.Dispose()
        {
            if (this.disposed)
            {
                return;
            }
            this.disposed = true;
            if (TimeWarning.OnEnd != null)
            {
                TimeWarning.OnEnd();
            }
            bool flag = this.gcCount != GC.CollectionCount(0);
            if (stopwatch.Elapsed.TotalMilliseconds >= 50)
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
            TimeWarning timeWarning = this;
            Pool.Free<TimeWarning>(ref timeWarning);
        }
    }
}
