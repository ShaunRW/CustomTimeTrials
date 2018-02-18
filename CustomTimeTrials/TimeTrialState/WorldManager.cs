using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;

using CustomTimeTrials.TimeTrialData;

namespace CustomTimeTrials.TimeTrialState
{
    class WorldManager
    {
        public void SetTimeOfDay(TimeTrialData.TimeOfDay timeOfDay)
        {
            TimeSpan time;
            switch (timeOfDay)
            {
                case TimeTrialData.TimeOfDay.Midnight:
                    time = new TimeSpan(00,00,00);
                    break;
                case TimeTrialData.TimeOfDay.PreDawn:
                    time = new TimeSpan(05, 00, 00);
                    break;
                case TimeTrialData.TimeOfDay.Dawn:
                    time = new TimeSpan(05, 00, 00);
                    break;
                case TimeTrialData.TimeOfDay.Morning:
                    time = new TimeSpan(08, 00, 00);
                    break;
                case TimeTrialData.TimeOfDay.Noon:
                    time = new TimeSpan(12, 00, 00);
                    break;
                case TimeTrialData.TimeOfDay.Afternoon:
                    time = new TimeSpan(16, 00, 00);
                    break;
                case TimeTrialData.TimeOfDay.Sunset:
                    time = new TimeSpan(18, 30, 00);
                    break;
                case TimeTrialData.TimeOfDay.Dusk:
                    time = new TimeSpan(21, 00, 00);
                    break;
                default:
                    time = new TimeSpan(12,00,00);
                    break;
            }
            World.CurrentDayTime = time;
        }

        public void SetWeather(Weather weather)
        {
            World.Weather = weather;
        }
    }
}
