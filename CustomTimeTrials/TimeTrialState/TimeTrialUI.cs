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
        public void ShowCountdownMessage(int countdownNumber, int duration=1000)
        {
            // Display the countdown number unless it is zero, then display 'GO'.
            string message = (countdownNumber == 0) ? "GO" : countdownNumber.ToString();
            BigMessageThread.MessageInstance.ShowOldMessage(message, duration);
        }
    }
}
