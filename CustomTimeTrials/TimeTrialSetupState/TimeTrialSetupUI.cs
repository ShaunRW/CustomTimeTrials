using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomTimeTrials.NativeMenu;

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

        public string GetSelectedTimeOfDay()
        {
            return this.menu.GetSelectedItem("Time of Day");
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
            List<dynamic> times = new List<dynamic>();
            times.Add("Midnight");
            times.Add("Pre-Dawn");
            times.Add("Dawn");
            times.Add("Morning");
            times.Add("Noon");
            times.Add("Afternoon");
            times.Add("Sunset");
            times.Add("Dusk");
            return times;
        }
    }
}
