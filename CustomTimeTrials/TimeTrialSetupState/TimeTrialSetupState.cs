using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomTimeTrials.StateMachine;
using CustomTimeTrials.MainMenuState;
using CustomTimeTrials.TimeTrialState;
using CustomTimeTrials.TimeTrialData;

namespace CustomTimeTrials.TimeTrialSetupState
{
    class TimeTrialSetupState : StateMachine.State
    {
        // utility properties
        private State newState;
        private GUI.SetupMenu setupMenu = new GUI.SetupMenu();

        // data properties
        private TimeTrialData.TimeTrialSaveData timeTrialData;

        public TimeTrialSetupState(TimeTrialData.TimeTrialSaveData raceData)
        {
            this.timeTrialData = raceData;
            this.setupMenu.CreateMenu(raceData.displayName, raceData.type, this.onMenuExit, this.onStart);
        }

        public override State onTick()
        {
            this.setupMenu.UpdateMenu();
            return this.newState;
        }

        /* MENU EVENT CALLBACKS 
        ======================== */
        private void onStart()
        {
            TimeTrialData.SetupData timeTrialSetup = new TimeTrialData.SetupData();

            // Get selected lap count if applicable.
            timeTrialSetup.lapCount = 0;
            if (this.timeTrialData.type == "circuit")
            {
                timeTrialSetup.lapCount = this.setupMenu.GetSelectedLapCount();
            }

            // get selected time of day.
            timeTrialSetup.timeOfDay = this.setupMenu.GetSelectedTimeOfDay();

            // get selected weather.
            timeTrialSetup.weather = this.setupMenu.GetSelectedWeather();

            // Get vehicle damage setting
            timeTrialSetup.vehicleDamageOn = this.setupMenu.GetSelectedVehicleDamage();

            // get traffic setting
            timeTrialSetup.trafficOn = this.setupMenu.GetSelectedTrafficOption();

            this.newState = new TimeTrialState.TimeTrialState(this.timeTrialData, timeTrialSetup);
        }
        private void onMenuExit()
        {
            this.newState = new MainMenuState.MainMenuState();
        }
    }
}
