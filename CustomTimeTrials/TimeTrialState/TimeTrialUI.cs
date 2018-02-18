using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;
using NativeUI;

namespace CustomTimeTrials.TimeTrialState
{
    class TimeTrialUI
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

        public void SetHUDTime(string time)
        {
            this.timeHud.Text = time;
        }

        public void SetHUDLap(string lap)
        {
            this.lapHud.Text = lap;
        }

        public void UpdateHUD()
        {
            this.hudPool.Draw();
        }



        public void ShowCountdownMessage(int countdownNumber, int duration=1000)
        {
            // Display the countdown number unless it is zero, then display 'GO'.
            string message = (countdownNumber == 0) ? "GO" : countdownNumber.ToString();
            BigMessageThread.MessageInstance.ShowOldMessage(message, duration);
        }

        public void ShowFinishedScreen(string title, string msg, int duration = 5000)
        {
            BigMessageThread.MessageInstance.ShowSimpleShard(title, msg, duration);
        }

        public void ShowFinishedNotification(string raceName, string finalTime, int lapCount=0)
        {
            string laps = (lapCount > 0) ? string.Format("  Laps: ~b~{0}\n~w~", lapCount) : "";
            string notification = string.Format("{0}:\n{1}  Final Time: ~b~{2}", raceName, laps, finalTime);
            UI.Notify(notification);
        }
    }
}
