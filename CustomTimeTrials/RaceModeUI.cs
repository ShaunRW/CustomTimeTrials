using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NativeUI;

namespace CustomTimeTrials
{
    class RaceModeUI
    {
        private TimerBarPool timerBarPool = new TimerBarPool();
        private TextTimerBar lapHud;
        private TextTimerBar timeHud;

        private int lastCountdownNumber;


        public RaceModeUI(int lapCount)
        {
            // Create the hud for the race time
            this.timeHud = new TextTimerBar("Time: ", "00:00");
            this.timerBarPool.Add(timeHud);

            // if more than one lap, create the hud for the lap counter.
            if (lapCount > 1)
            {
                this.lapHud = new TextTimerBar("Lap:", "1/" + lapCount.ToString());
                this.timerBarPool.Add(lapHud);
            }
        }

        
        public void update()
        {
            this.timerBarPool.Draw();
        }


        public void SetLap(int lap, int lapCount)
        {
            this.lapHud.Text = string.Format("{0}/{1}", lap, lapCount);
        }


        public void SetTime(int ms)
        {
            this.timeHud.Text = TimeSpan.FromMilliseconds(ms).ToString((@"mm\:ss"));
        }


        public void ShowLargeMessage(string msg, int duration=5000)
        {
            BigMessageThread.MessageInstance.ShowMissionPassedMessage(msg, duration);
        }

        public void ShowCountdown(int timeLeft)
        {
            if (timeLeft <= 0)
            {
                this.ShowCountdownSeconds(0);
            }
            else if (timeLeft <= 1000)
            {
                this.ShowCountdownSeconds(1);
            }
            else if (timeLeft <= 2000)
            {
                this.ShowCountdownSeconds(2);
            }
            else if (timeLeft <= 3000)
            {
                this.ShowCountdownSeconds(3);
            }
            else
            {
                this.lastCountdownNumber = 4;
            }
        }

        public void ShowCountdownSeconds(int seconds, int duration = 1000)
        {
            if (this.lastCountdownNumber > seconds)
            {
                string text;

                if (seconds == 0)
                {
                    text = "GO";
                }
                else
                {
                    text = seconds.ToString();
                }

                BigMessageThread.MessageInstance.ShowOldMessage(text, duration);
                this.lastCountdownNumber = seconds;
            }
        }
    }
}
