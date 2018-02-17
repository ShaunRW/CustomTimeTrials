using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;

using CustomTimeTrials.StateMachine;

namespace CustomTimeTrials.TimeTrialState
{
    class TimeTrialState : StateMachine.State
    {
        // Utility Properties
        private State newState;
        // TimeTrialState internal states
        private enum InternalState {Countdown, Race}
        private InternalState innerState;

        private TimeTrialUI timeTrialUI = new TimeTrialUI();

        private Countdown countdown = new Countdown(4, 1500);

        private TimeManager time;
        private TimeTrialData timeTrialData;
        private LapManager lapManager;
        private CheckpointManager checkpointManager;
        private PlayerManager player = new PlayerManager();


        public TimeTrialState(TimeTrialData data, int lapCount)
        {
            // init time trial managers
            this.timeTrialData = data;
            this.lapManager = new LapManager(lapCount, data.type, this.onNewLap, this.onFinish);
            this.checkpointManager = new CheckpointManager(this.onCheckpointReached, this.onLapComplete);

            // setup the time trial
            this.SetupTimeTrial();

            // begin the race countdown
            this.StartCountdown();
        }

        public override State onTick()
        {
            if (this.innerState == InternalState.Race)
            {
                this.UpdateTimeTrial();
            }
            else if (this.innerState == InternalState.Countdown)
            {
                if (this.countdown.Update())
                {
                    this.BeginTimeTrial();
                }
            }
            this.timeTrialUI.UpdateHUD();
            return this.newState;
        }



        private void StartCountdown()
        {
            this.countdown.Begin();
            this.innerState = InternalState.Countdown;
            this.player.FreezePlayer();
        }



        private void SetupTimeTrial()
        {
            this.checkpointManager.Load(this.timeTrialData);

            this.player.MoveVehicleTo(this.timeTrialData.start.position.ToGtaVector3(), this.timeTrialData.start.rotation.ToGtaVector3());

            this.checkpointManager.Show(1, CheckpointIcon.Arrow);
            this.checkpointManager.ShowFutureBlip();

            this.timeTrialUI.SetupTimeHud();
            if (this.lapManager.isCircuit)
            {
                this.timeTrialUI.SetupLapHud(this.lapManager.ToString());
            }
        }
        
        private void BeginTimeTrial()
        {
            this.innerState = InternalState.Race;
            this.player.UnfreezePlayer();
            this.time = new TimeManager();
        }



        private void UpdateTimeTrial()
        {
            this.timeTrialUI.SetHUDTime(this.time.Format());
            this.checkpointManager.Update(this.lapManager.onLast);
        }



        private void onCheckpointReached()
        {
            UI.Notify("onCheckpointReached");
        }

        private void onLapComplete()
        {
            UI.Notify("onLapComplete");
            this.lapManager.EndCurrentLap();
        }

        private void onNewLap()
        {
            this.timeTrialUI.SetHUDLap(this.lapManager.ToString());
        }

        private void onFinish()
        {
            UI.Notify("onFinish");
            this.newState = new InactiveState.InactiveState();
        }
    }
}
