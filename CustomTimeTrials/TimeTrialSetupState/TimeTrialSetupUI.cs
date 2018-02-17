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

        public void CreateMenu(string raceName, string raceType, Action onMenuExitCallback)
        {
            this.menu = new NativeMenu.Menu("Custom Time Trials", raceName, onMenuExitCallback);

            if (raceType=="circuit")
            {
                this.menu.AddListButton("Laps", this.GenerateLapsOptions());
            }
            this.menu.AddButton("Start", null);

            this.menu.Show();
        }

        public void UpdateMenu()
        {
            this.menu.Update();
        }

        private List<dynamic> GenerateLapsOptions(int min = 1, int max = 100)
        {
            List<dynamic> laps = new List<dynamic>();
            for (int i = min; i < max; i++)
            {
                laps.Add(i.ToString());
            }
            return laps;
        }
    }
}
