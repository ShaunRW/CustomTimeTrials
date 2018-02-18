using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;

namespace CustomTimeTrials.TimeTrialState
{
    class WorldManager
    {
        public void SetTimeOfDay(string timeOfDay)
        {
            TimeSpan time;
            switch (timeOfDay)
            {
                case "Midnight":
                    time = new TimeSpan(00,00,00);
                    break;
                case "Pre-Dawn":
                    time = new TimeSpan(05, 00, 00);
                    break;
                case "Dawn":
                    time = new TimeSpan(05, 00, 00);
                    break;
                case "Morning":
                    time = new TimeSpan(08, 00, 00);
                    break;
                case "Noon":
                    time = new TimeSpan(12, 00, 00);
                    break;
                case "Afternoon":
                    time = new TimeSpan(16, 00, 00);
                    break;
                case "Sunset":
                    time = new TimeSpan(18, 30, 00);
                    break;
                case "Dusk":
                    time = new TimeSpan(21, 00, 00);
                    break;
                default:
                    time = new TimeSpan(12,00,00);
                    break;
            }
            World.CurrentDayTime = time;
        }
    }
}
