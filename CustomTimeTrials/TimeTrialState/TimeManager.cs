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
    }
}
