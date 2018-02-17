using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;

using CustomTimeTrials.StateMachine;
using CustomTimeTrials.InactiveState;
using CustomTimeTrials.EditorState;

namespace CustomTimeTrials.MainMenuState
{
    class MainMenuState : StateMachine.State
    {
        private State newState = null;
        private MainMenuUI mainMenuUI = new MainMenuUI();

        public MainMenuState()
        {
            this.mainMenuUI.CreateMenu(this.onMenuExit, this.onSelectStartTimeTrial, this.onSelectEditor);
        }

        public override State onTick()
        {
            this.mainMenuUI.UpdateMenu();
            return this.newState;
        }


        /* MENU EVENT CALLBACKS 
        ======================== */
        private void onSelectStartTimeTrial()
        {
            // Start race
        }

        private void onSelectEditor()
        {
            // this.newState = new EditorState.EditorState();
        }

        private void onMenuExit()
        {
            this.newState = new InactiveState.InactiveState();
        }
    }
}
