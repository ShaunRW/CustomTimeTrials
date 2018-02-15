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


        public string GetTimeAsString(int ms, bool includeMs=false)
        {
            if (includeMs)
            {
                return TimeSpan.FromMilliseconds(ms).ToString((@"mm\:ss\:fff"));
            }
            else
            {
                return TimeSpan.FromMilliseconds(ms).ToString((@"mm\:ss"));
            }
        }


        public void SetTime(int ms)
        {
            this.timeHud.Text = this.GetTimeAsString(ms);
        }


        public void ShowLargeMessage(string title, string msg, int duration=5000)
        {
            BigMessageThread.MessageInstance.ShowSimpleShard(title, msg, duration);
        }

        public bool ShowCountdown(int timeLeft)
        {
            int stepNumber;
            if (timeLeft <= 0)
            {
                stepNumber = 0;
            }
            else if (timeLeft <= 1500)
            {
                stepNumber = 1;
            }
            else if (timeLeft <= 3000)
            {
                stepNumber = 2;
            }
            else if (timeLeft <= 4500)
            {
                stepNumber = 3;
            }
            else
            {
                stepNumber = 5;
                this.lastCountdownNumber = 4;
            }

            bool displayed = this.ShowCountdownStep(stepNumber);
            return displayed;
        }

        private bool ShowCountdownStep(int number, int duration = 1000)
        {
            if (this.lastCountdownNumber > number)
            {
                string text;

                if (number == 0)
                {
                    text = "GO";
                }
                else
                {
                    text = number.ToString();
                }

                BigMessageThread.MessageInstance.ShowOldMessage(text, duration);
                this.lastCountdownNumber = number;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
