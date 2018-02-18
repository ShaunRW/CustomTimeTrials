﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CustomTimeTrials.StateMachine;
using CustomTimeTrials.TimeTrialData;

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
        private TimeTrialData.TimeTrialSaveData timeTrialData;
        private LapManager lapManager;
        private CheckpointManager checkpointManager;
        private WorldManager world = new WorldManager();
        private PlayerManager player = new PlayerManager();
        private TimeTrialAudio audioManager = new TimeTrialAudio();


        public TimeTrialState(TimeTrialData.TimeTrialSaveData data, TimeTrialData.SetupData setup)
        {
            // init time trial data and checkpoints
            this.timeTrialData = data;
            this.checkpointManager = new CheckpointManager(this.onCheckpointReached, this.onLapComplete);

            // Process the setup data
            this.lapManager = new LapManager(setup.lapCount, data.type, this.onNewLap, this.onFinish);
            this.world.SetTimeOfDay(setup.timeOfDay);
            this.world.SetWeather(setup.weather);

            

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

            this.player.CantDie();
            this.player.SetVehicleDamageOn(false); // this will be optional soon.

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
            this.audioManager.PlayRaceBeginSound();
        }



        private void UpdateTimeTrial()
        {
            this.timeTrialUI.SetHUDTime(this.time.Format());
            this.checkpointManager.Update(this.lapManager.onLast);

            this.player.HealPlayerIfDamaged();
            this.player.FixVehicleIfDamaged(); // this will be optional soon.
        }

        private void ResetStates()
        {
            this.player.CantDie(false);
            this.player.SetVehicleDamageOn();
        }



        private void onCheckpointReached()
        {
            this.audioManager.PlayCheckpointReachedSound();
        }

        private void onLapComplete()
        {
            this.lapManager.EndCurrentLap();
            // todo: record fastest laps.
        }

        private void onNewLap()
        {
            this.audioManager.PlayCheckpointReachedSound();
            this.timeTrialUI.SetHUDLap(this.lapManager.ToString());
        }

        private void onFinish()
        {
            // do something with the race data
            string time = this.time.Format(true);
            this.audioManager.PlayRaceFinishedSound();
            this.timeTrialUI.ShowFinishedScreen("Finished", time);
            this.timeTrialUI.ShowFinishedNotification( this.timeTrialData.displayName, time, this.lapManager.count);

            this.ResetStates();

            this.newState = new InactiveState.InactiveState();
        }
    }
}
