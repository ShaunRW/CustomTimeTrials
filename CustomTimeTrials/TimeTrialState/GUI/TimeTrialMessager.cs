using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;
using NativeUI;

namespace CustomTimeTrials.TimeTrialState.GUI
{
    class TimeTrialMessager
    {
        public void Notify(string message, bool blinking=false)
        {
            UI.Notify(message, blinking);
        }


        public void ShowCountdownMessage(int countdownNumber, int duration = 1000)
        {
            // Display the countdown number unless it is zero, then display 'GO'.
            string message = (countdownNumber == 0) ? "GO" : countdownNumber.ToString();
            BigMessageThread.MessageInstance.ShowOldMessage(message, duration);
        }

        public void ShowFinishedScreen(string title, string msg, int duration = 5000)
        {
            BigMessageThread.MessageInstance.ShowSimpleShard(title, msg, duration);
        }

        public void ShowFinishedNotification(string raceName, string fastestLapTime, string averageLapTime, string finalTime, int lapCount = 0)
        {
            // Build the lap details string if lapCount is greater than zero.
            string lapsInfo = (lapCount > 0)
                ? string.Format("  Laps: ~b~{0}\n~w~", lapCount)
                : "";

            // Add fastest and average lap times if laps are counted.
            string lapTimesInfo = (lapCount > 0)
                ? string.Format("  Fastest Lap: ~b~{0}\n~w~  Average Lap: ~b~{1}\n~w~", fastestLapTime, averageLapTime)
                : "";

            // Combine all information into the notification message.
            string notification = string.Format(
                "{0}:\n{1}{2}  Final Time: ~b~{3}",
                raceName,
                lapsInfo,
                lapTimesInfo,
                finalTime
            );

            // Display the notification.
            this.Notify(notification);
        }

    }
}
