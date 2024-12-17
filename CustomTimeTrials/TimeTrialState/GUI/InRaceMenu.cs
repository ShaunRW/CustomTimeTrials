using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomTimeTrials.NativeMenu;

namespace CustomTimeTrials.TimeTrialState.GUI
{
    class InRaceMenu
    {
        private NativeMenu.Menu menu;

        public InRaceMenu(Action onMenuExitCallback, Action onRespawnCallback, Action onRestartCallback, Action onExitCallback)
        {
            this.menu = new NativeMenu.Menu("Custom Time Trials", "In Race Menu", onMenuExitCallback);

            this.menu.AddButton("Respawn at Last Checkpoint", onRespawnCallback);
            this.menu.AddButton("Restart", onRestartCallback);
            this.menu.AddButton("Exit Time Trial", onExitCallback);

            this.menu.SetMenuIsReady();
        }

        public void Toggle()
        {
            this.menu.Toggle();
        }

        public void Update()
        {
            this.menu.Update();
        }
    }
}
