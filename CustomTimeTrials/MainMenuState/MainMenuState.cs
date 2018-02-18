using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;

using CustomTimeTrials.StateMachine;
using CustomTimeTrials.InactiveState;
using CustomTimeTrials.TimeTrialSetupState;
using CustomTimeTrials.EditorState;
using CustomTimeTrials.TimeTrialData;

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
            // load the selected time trial data
            string timeTrial = this.mainMenuUI.GetSelectedTimeTrial();
            TimeTrialData.TimeTrialFile file = new TimeTrialData.TimeTrialFile();
            file.load(timeTrial);

            this.newState = new TimeTrialSetupState.TimeTrialSetupState(file.data);
        }

        private void onSelectEditor()
        {
            this.newState = new EditorState.EditorState();
        }

        private void onMenuExit()
        {
            this.newState = new InactiveState.InactiveState();
        }
    }
}
