using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NativeUI;

namespace CustomTimeTrials.TimeTrialState.GUI
{
    class TimeTrialHUD
    {
        private TimerBarPool hudPool = new TimerBarPool();
        private TextTimerBar timeHud;
        private TextTimerBar lapHud;

        public void SetupTimeHud()
        {
            this.timeHud = new TextTimerBar("Time:", "00:00");
            this.hudPool.Add(this.timeHud);
        }

        public void SetupLapHud(string lap)
        {
            this.lapHud = new TextTimerBar("Lap:", lap);
            this.hudPool.Add(this.lapHud);
        }

        public void SetTime(string time)
        {
            this.timeHud.Text = time;
        }

        public void SetLap(string lap)
        {
            this.lapHud.Text = lap;
        }

        public void Update()
        {
            this.hudPool.Draw();
        }
    }
}
