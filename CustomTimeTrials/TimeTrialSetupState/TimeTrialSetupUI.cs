using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;

using CustomTimeTrials.NativeMenu;
using CustomTimeTrials.TimeTrialData;

namespace CustomTimeTrials.TimeTrialSetupState
{
    class TimeTrialSetupUI
    {
        private NativeMenu.Menu menu;

        public void CreateMenu(string raceName, string raceType, Action onMenuExitCallback, Action onStartCallback)
        {
            this.menu = new NativeMenu.Menu("Custom Time Trials", raceName, onMenuExitCallback);

            if (raceType=="circuit")
            {
                this.menu.AddListButton("Laps", this.GenerateLapsOptions());
            }
            this.menu.AddListButton("Time of Day", this.GenerateTimeOfDayOptions());
            this.menu.AddListButton("Weather", this.GenerateWeatherOptions());
            this.menu.AddListButton("Vehicle Damage", new List<dynamic>{"On", "Off"});
            this.menu.AddButton("Start", onStartCallback);

            this.menu.Show();
        }

        public void UpdateMenu()
        {
            this.menu.Update();
        }

        public int GetSelectedLapCount()
        {
            return this.menu.GetSelectedItem("Laps");
        }

        public TimeTrialData.TimeOfDay GetSelectedTimeOfDay()
        {
            return this.menu.GetSelectedItem("Time of Day");
        }

        public Weather GetSelectedWeather()
        {
            return this.menu.GetSelectedItem("Weather");
        }

        public bool GetSelectedVehicleDamage()
        {
            if (this.menu.GetSelectedItem("Vehicle Damage") == "On")
            {
                return true;
            }
            return false;
        }

        private List<dynamic> GenerateLapsOptions(int min = 1, int max = 100)
        {
            List<dynamic> laps = new List<dynamic>();
            for (int i = min; i < max; i++)
            {
                laps.Add(i);
            }
            return laps;
        }

        private List<dynamic> GenerateTimeOfDayOptions()
        {
            var times = new List<dynamic>();
            times.Add(TimeTrialData.TimeOfDay.Morning);
            times.Add(TimeTrialData.TimeOfDay.Noon);
            times.Add(TimeTrialData.TimeOfDay.Afternoon);
            times.Add(TimeTrialData.TimeOfDay.Sunset);
            times.Add(TimeTrialData.TimeOfDay.Dusk);
            times.Add(TimeTrialData.TimeOfDay.Midnight);
            times.Add(TimeTrialData.TimeOfDay.PreDawn);
            times.Add(TimeTrialData.TimeOfDay.Dawn);
            return times;
        }

        private List<dynamic> GenerateWeatherOptions()
        {
            var weather = new List<dynamic>();
            weather.Add(Weather.Clear);
            weather.Add(Weather.ExtraSunny);
            weather.Add(Weather.Clouds);
            weather.Add(Weather.Overcast);
            weather.Add(Weather.Raining);
            weather.Add(Weather.ThunderStorm);
            weather.Add(Weather.Snowing);
            weather.Add(Weather.Blizzard);
            return weather;
        }
    }
}
