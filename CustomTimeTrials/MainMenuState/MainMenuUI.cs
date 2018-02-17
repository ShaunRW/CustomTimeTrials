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

        public void CreateMenu(Action onMenuExitCallback)
        {
            this.menu = new NativeMenu.Menu("Custom Time Trials", "Main Menu", onMenuExitCallback);

            this.menu.AddButton("Start Time Trial", null);
            this.menu.AddButton("Time Trial Editor", null);

            this.menu.Show();
        }

        public void UpdateMenu()
        {
            this.menu.Update();
        }
    }
}
