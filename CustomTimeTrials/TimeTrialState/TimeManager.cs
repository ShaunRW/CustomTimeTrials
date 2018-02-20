using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;

namespace CustomTimeTrials.TimeTrialState
{
    class TimeManager
    {
        private int start;

        public int now
        {
            get { return Game.GameTime; }
        }

        public int elapsed
        {
            get { return this.now - this.start; }
        }

        public TimeManager()
        {
            this.Reset();
        }

        public void Reset()
        {
            this.start = this.now;
        }

        public string ToString(bool includeMilliseconds=false)
        {
            return TimeManager.Format(this.elapsed, includeMilliseconds);
        }

        public static string Format(int milliseconds, bool includeMilliseconds=false)
        {
            if (includeMilliseconds)
            {
                return TimeSpan.FromMilliseconds(milliseconds).ToString((@"mm\:ss\:fff"));
            }
            else
            {
                return TimeSpan.FromMilliseconds(milliseconds).ToString((@"mm\:ss"));
            }
        }
    }
}
