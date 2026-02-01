using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomTimeTrials.NativeMenu;

namespace CustomTimeTrials.MainMenuState.GUI
{
    class MainMenu
    {
        private NativeMenu.Menu menu;

        public void CreateMenu(Action onMenuExitCallback, Action onStartTimeTrialCallback, Action onStartEditorCallback)
        {
            this.menu = new NativeMenu.Menu("Custom Time Trials", "Main Menu", onMenuExitCallback);

            List<dynamic> timeTrialList = this.GetTimeTrialList();
            if (timeTrialList.Count > 0)
            {
                this.menu.AddListButton("Start Time Trial", timeTrialList, onStartTimeTrialCallback);
            }
            this.menu.AddButton("Time Trial Editor", onStartEditorCallback);

            this.menu.Show();
        }

        public void UpdateMenu()
        {
            this.menu.Update();
        }

        public string GetSelectedTimeTrial()
        {
            return this.menu.GetSelectedItem("Start Time Trial");
        }

        private List<dynamic> GetTimeTrialList(bool includeExtension = false)
        {
            List<dynamic> races = new List<dynamic>();
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string targetDir = System.IO.Path.Combine(documentsPath, "Rockstar Games", "GTAV-CustomTimeTrials", "TimeTrials");
            if (!System.IO.Directory.Exists(targetDir))
            {
                System.IO.Directory.CreateDirectory(targetDir);
            }
            string[] files = System.IO.Directory.GetFiles(targetDir);
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
