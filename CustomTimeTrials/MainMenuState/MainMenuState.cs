using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;

using CustomTimeTrials.StateMachine;
using CustomTimeTrials.InactiveState;

namespace CustomTimeTrials.MainMenuState
{
    class MainMenuState : StateMachine.State
    {
        private State newState = null;
        private MainMenuUI mainMenuUI = new MainMenuUI();

        public MainMenuState()
        {
            this.mainMenuUI.CreateMenu(this.onMenuExit);
        }

        public override State onTick()
        {
            this.mainMenuUI.UpdateMenu();
            return this.newState;
        }

        private void onMenuExit()
        {
            UI.Notify("Exit Menu");
            this.newState = new InactiveState.InactiveState();
        }
    }
}
