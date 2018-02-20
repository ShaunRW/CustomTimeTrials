using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        private Countdown countdown = new Countdown(4, 1500);

        private TimeTrialData.TimeTrialSaveData timeTrialData;
        private TimeTrialData.SetupData setup;
        private LapManager lapManager;

        private TimeManager time;
        private CheckpointManager checkpointManager;
        private WorldManager world = new WorldManager();
        private PlayerManager player = new PlayerManager();
        private TimeTrialAudio audioManager = new TimeTrialAudio();

        private KeyPressTracker keyPressTracker = new KeyPressTracker();

        private GUI.TimeTrialMessager messager = new GUI.TimeTrialMessager();
        private GUI.TimeTrialHUD HUD = new GUI.TimeTrialHUD();
        private GUI.InRaceMenu inRaceMenu;


        public TimeTrialState(TimeTrialData.TimeTrialSaveData data, TimeTrialData.SetupData setup)
        {
            // init time trial data and checkpoints
            this.timeTrialData = data;
            this.setup = setup;

            // In Race Menu
            this.inRaceMenu = new GUI.InRaceMenu(this.onInRaceMenuExit, this.RestartTimeTrial, this.ExitTimeTrial);

            // setup the time trial
            this.SetupTimeTrial();

            // begin the race countdown
            this.StartCountdown();
        }

        public override State onTick()
        {
            // update the countdown or time trial state.
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

            // draw hud to the screen
            this.HUD.Update();


            // handle the in race menu
            this.inRaceMenu.Update();

            return this.newState;
        }

        public override State onKeyDown(KeyEventArgs e)
        {
            bool justPressed = this.keyPressTracker.update(e.KeyCode, true);

            if (justPressed && e.KeyCode == Keys.F9)
            {
                this.inRaceMenu.Toggle();
            }

            return this.newState;
        }

        /*
         * this is needed so the keypress tracker can update the key states,
         */
        public override State onKeyUp(KeyEventArgs e)
        {
            bool justPressed = this.keyPressTracker.update(e.KeyCode, false);
            return null;
        }



        private void StartCountdown()
        {
            this.countdown.Begin();
            this.innerState = InternalState.Countdown;
            this.player.FreezePlayer();
        }



        private void SetupTimeTrial()
        {
            /*
             * Setup Race Data
             */
            this.lapManager = new LapManager(this.setup.lapCount, this.timeTrialData.type, this.onNewLap, this.onFinish);
            
            // Load Checkpoints
            this.checkpointManager = new CheckpointManager(this.onCheckpointReached, this.onLapComplete);
            this.checkpointManager.Load(this.timeTrialData);

            // Show initial checkpoints
            this.checkpointManager.Show(1, CheckpointIcon.Arrow);
            this.checkpointManager.ShowFutureBlip();


            /*
             * Setup Environment
             */
            this.world.SetWeather(this.setup.weather);
            this.world.SetTimeOfDay(this.setup.timeOfDay);
            if (this.setup.trafficOn == false)
            {
                this.world.SetTrafficOff();
            }


            /*
             * Setup Player and Vehicle
             */
            this.player.CantDie();
            this.player.MoveVehicleTo(this.timeTrialData.start.position.ToGtaVector3(), this.timeTrialData.start.rotation.ToGtaVector3());
            this.player.SetVehicleDamageOn(this.setup.vehicleDamageOn);


            /*
             * Setup Hud.
             *  - Should this be here or in the constructor.
             */
            this.HUD.SetupTimeHud();
            if (this.lapManager.isCircuit)
            {
                this.HUD.SetupLapTimeHud();
                this.HUD.SetupFastestLapTimeHud();
                this.HUD.SetupLapHud(this.lapManager.ToString());
            }
        }
        

        private void BeginTimeTrial()
        {
            this.innerState = InternalState.Race;
            this.player.UnfreezePlayer();
            this.time = new TimeManager();
            this.lapManager.AddLap();
            this.audioManager.PlayRaceBeginSound();
        }



        private void UpdateTimeTrial()
        {
            this.HUD.SetTime(this.time.ToString());
            this.checkpointManager.Update(this.lapManager.onLast);

            this.player.HealPlayerIfDamaged();
            if (!this.setup.vehicleDamageOn)
            {
                this.player.FixVehicleIfDamaged();
            }
        }

        private void UnloadTimeTrial()
        {
            // player can die again and vehicle can get damaged
            this.player.CantDie(false);
            this.player.SetVehicleDamageOn();

            // if traffic was turned off, turn it back on again.
            if (this.setup.trafficOn == false)
            {
                this.world.SetTrafficOn();
            }

            // remove all checkpoints.
            this.checkpointManager.UnloadAllCheckpoints();
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
            this.HUD.SetLap(this.lapManager.ToString());
        }

        private void onFinish()
        {
            // do something with the race data
            string time = this.time.ToString(true);
            this.audioManager.PlayRaceFinishedSound();
            this.messager.ShowFinishedScreen("Finished", time);
            this.messager.ShowFinishedNotification( this.timeTrialData.displayName, time, this.lapManager.count);

            this.ExitTimeTrial();
        }



        /* Do nothing:
             *  this method needs to be passed to the InRaceMenu,
             *  but since we don't want anything to happen we leave this empty.
             */
        private void onInRaceMenuExit() { }

        private void RestartTimeTrial()
        {
            this.messager.Notify("Restarted Time Trial", true);
            this.ExitTimeTrial(new TimeTrialState(this.timeTrialData, this.setup));
        }

        /* 
         * this is a wrapper so it can be used as an Action argument.
         */
        private void ExitTimeTrial()
        {
            this.ExitTimeTrial(null);
        }

        private void ExitTimeTrial(State newState)
        {
            this.UnloadTimeTrial();

            if (newState == null)
            {
                newState = new InactiveState.InactiveState();
            }
            this.newState = newState;
        }
    }
}
