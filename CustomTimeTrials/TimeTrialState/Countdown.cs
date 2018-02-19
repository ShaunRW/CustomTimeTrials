using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomTimeTrials.TimeTrialState
{
    class Countdown
    {

        private GUI.TimeTrialMessager messager = new GUI.TimeTrialMessager();
        private TimeTrialAudio audioManager = new TimeTrialAudio();

        private TimeManager time;
        private int currentStep;
        private int interval;

        public Countdown(int from, int interval)
        {
            this.currentStep = from;
            this.interval = interval;
        }

        public void Begin()
        {
            this.time = new TimeManager();
        }

        public bool Update()
        {
            int timeLeft = this.interval - this.time.elapsed;

            if (timeLeft <= 0)
            {
                this.currentStep -= 1;

                this.messager.ShowCountdownMessage(this.currentStep);
                this.audioManager.PlayCountdownBeep();

                if (this.currentStep == 0)
                {
                    return true;
                }
                else
                {
                    this.time.Reset();
                }
            }
            return false;
        }
    }
}
