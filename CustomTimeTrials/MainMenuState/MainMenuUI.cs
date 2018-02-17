using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomTimeTrials.NativeMenu;

namespace CustomTimeTrials.MainMenuState
{
    class MainMenuUI
    {
        private NativeMenu.Menu menu;

        public void CreateMenu(Action onMenuExitCallback, Action onStartTimeTrialCallback, Action onStartEditorCallback)
        {
            this.menu = new NativeMenu.Menu("Custom Time Trials", "Main Menu", onMenuExitCallback);

            this.menu.AddListButton("Start Time Trial", this.GetTimeTrialList(), onStartTimeTrialCallback);
            this.menu.AddButton("Time Trial Editor", onStartEditorCallback);

            this.menu.Show();
        }

        public void UpdateMenu()
        {
            this.menu.Update();
        }

        private List<dynamic> GetTimeTrialList(bool includeExtension = false)
        {
            List<dynamic> races = new List<dynamic>();
            string[] files = System.IO.Directory.GetFiles("scripts/TimeTrials");
            foreach (string file in files)
            {
                if (includeExtension)
                {
                    races.Add(System.IO.Path.GetFileName(file));
                }
                else
                {
                    races.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                }
            }
            return races;
        }
    }
}
