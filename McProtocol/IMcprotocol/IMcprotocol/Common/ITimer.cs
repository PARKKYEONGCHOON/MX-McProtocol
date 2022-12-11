using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentFactory
{
    public class ITimer
    {
        private Stopwatch m_sw = new Stopwatch();

        public ITimer()
        {
            m_sw.Start();
        }

        public void Reset()
        {
            m_sw.Restart();
        }

        public long Elapsed()
        {
            long lElapsed = m_sw.ElapsedMilliseconds;
            return (lElapsed);
        }

        public bool Delay(long lTime_ms)
        {
            if (lTime_ms < m_sw.ElapsedMilliseconds)
                return (true);
            else
                return (false);
        }

        public static double GetSystemTickHour()
        {
            DateTime currentDate = DateTime.Now;

            double dTicks = currentDate.Ticks / 10000000.0; // 1Ticks = 100 nano-sec 
            dTicks = dTicks / 3600.0; // sec->hour
            return (dTicks);
        }
    }
}
