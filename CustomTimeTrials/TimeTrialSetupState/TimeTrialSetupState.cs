using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomTimeTrials.StateMachine;
using CustomTimeTrials.MainMenuState;
using CustomTimeTrials.TimeTrialState;

namespace CustomTimeTrials.TimeTrialSetupState
{
    class TimeTrialSetupState : StateMachine.State
    {
        // utility properties
        private State newState;
        private TimeTrialSetupUI setupUI = new TimeTrialSetupUI();

        // data properties
        private TimeTrialData timeTrialData;

        public TimeTrialSetupState(TimeTrialData raceData)
        {
            this.timeTrialData = raceData;
            this.setupUI.CreateMenu(raceData.displayName, raceData.type, this.onMenuExit, this.onStart);
        }

        public override State onTick()
        {
            this.setupUI.UpdateMenu();
            return this.newState;
        }

        /* MENU EVENT CALLBACKS 
        ======================== */
        private void onStart()
        {
            int lapCount = this.setupUI.GetSelectedLapCount();
            this.newState = new TimeTrialState.TimeTrialState(this.timeTrialData, lapCount);
        }
        private void onMenuExit()
        {
            this.newState = new MainMenuState.MainMenuState();
        }
    }
}
