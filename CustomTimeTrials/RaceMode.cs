using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GTA;
using GTA.Math;

namespace CustomTimeTrials
{

    class RaceMode : Mode
    {
        private RaceModeUI raceModeUI;
        private RaceModeAudio raceModeAudio = new RaceModeAudio();
        private CheckpointManager checkpointManager = new CheckpointManager();

        private Vector3 startingRotation;

        private string raceName;
        private bool isCircuit;
        private int lapCount;
        private int currentLap;

        private bool isLastLap
        {
            get { return this.currentLap == this.lapCount; }
        }

        private bool countingDown;
        private int startingTime;
        private int elapsedTime
        {
            get { return Game.GameTime - this.startingTime; }
        }

        public RaceMode(string raceName, int laps, KeyPressTracker keyPressTracker) : base(keyPressTracker)
        {
            this.raceName = System.IO.Path.GetFileNameWithoutExtension(raceName);
            if (Game.Player.Character.IsSittingInVehicle())
            {
                if (this.LoadRace("scripts/TimeTrials/" + raceName))
                {
                    this.SetupRace(laps);
                }
                else
                {
                    UI.Notify("Race could not be loaded.");
                    this.ExitRace();
                }
            }
            else
            {
                UI.Notify("You need to be in a ~r~vehicle ~w~before you can race.");
                this.ExitRace();
            }
        }

        private void SetupRace(int laps)
        {
            // initialize the lap counter.
            if (this.isCircuit)
            {
                this.lapCount = laps;
            }
            else
            {
                this.lapCount = 1;
            }
            this.currentLap = 1;

            // put player at the race start
            this.MoveVehicleToStartingLine();

            // Show first checkpoint and the future blip
            this.checkpointManager.Show(1, CheckpointIcon.Arrow);
            this.checkpointManager.ShowFutureBlip();

            // initialize the race hud
            this.raceModeUI = new RaceModeUI(this.lapCount);

            // begin the countdown
            this.BeginCountDown();
        }

        private void MoveVehicleToStartingLine()
        {
            Vehicle vehicle = Game.Player.LastVehicle;
            vehicle.Position = this.checkpointManager.GetByIndex(0).position;
            vehicle.Rotation = this.startingRotation;
            GameplayCamera.RelativeHeading = 0.0f;
        }

        private void BeginCountDown()
        {
            this.countingDown = true;
            this.startingTime = Game.GameTime;
            Game.Player.CanControlCharacter = false;

        }

        private void UpdateCountdown()
        {
            int timeLeft = 6000 - (Game.GameTime - this.startingTime);
            if (this.raceModeUI.ShowCountdown(timeLeft))
            {
                if (timeLeft <= 0)
                {
                    this.EndCountdown();
                }
                else
                {
                    this.raceModeAudio.PlayCountdownBeep();
                }
            }
            
        }

        private void EndCountdown()
        {
            this.countingDown = false;
            this.BeginRace();
        }

        private void BeginRace()
        {
            this.startingTime = Game.GameTime;
            Game.Player.CanControlCharacter = true;
            this.raceModeAudio.PlayRaceBeginSound();
        }

        private void AddLap()
        {
            this.ShowNextCheckpoint();
            this.currentLap++;
            this.raceModeUI.SetLap(this.currentLap, this.lapCount);
        }

        private void EndLap()
        {
            if (this.currentLap == this.lapCount)
            {
                this.raceModeAudio.PlayRaceFinishedSound();
                this.EndRace();
            }
            else
            {
                this.raceModeAudio.PlayCheckpointReachedSound();
                this.AddLap();
            }
        }

        private void EndRace()
        {
            string finalTime = this.raceModeUI.GetTimeAsString(this.elapsedTime, true);
            string laps = (this.isCircuit) ? string.Format("  Laps: ~b~{0}\n~w~", this.lapCount) : "";

            this.raceModeUI.ShowLargeMessage("Finished", finalTime);

            string notification = string.Format("{0}:\n{1}  Final Time: ~b~{2}", this.raceName, laps, finalTime);

            UI.Notify(notification);
            this.ExitRace();
        }

        private void UpdateRace()
        {
            CheckpointManagerEvent cpEvent = this.checkpointManager.GetEvent();

            if (cpEvent == CheckpointManagerEvent.TargetReached)
            {
                this.ShowNextCheckpoint();
            }
            else if (cpEvent == CheckpointManagerEvent.LapEndReached)
            {
                this.EndLap();
            }

            this.raceModeUI.SetTime(this.elapsedTime);
        }

        private void ShowNextCheckpoint()
        {
            this.raceModeAudio.PlayCheckpointReachedSound();
            this.checkpointManager.TargetNext(this.isLastLap);
        }

        public override void onTick()
        {
            if (this.countingDown)
            {
                this.UpdateCountdown();
            }
            else
            {
                this.UpdateRace();
            }

            this.raceModeUI.update();
        }

        public override void onKeyDown(KeyEventArgs e)
        {
            bool justPressed = this.KeyPress.update(e.KeyCode, true);

            if (justPressed && e.KeyCode == Keys.F10)
            {
                UI.Notify("Exited Time Trial");
                this.ExitRace();
            }
        }
        

        private bool LoadRace(string path)
        {
            string[] vectorsStr;
            var isFirstLine = true;
            var checkpointPositions = new List<Vector3>();

            // make sure file exists before extracting the vectors
            if (System.IO.File.Exists(path))
            {
                string fileExt = System.IO.Path.GetExtension(path);
                if (fileExt == ".circuit")
                {
                    this.isCircuit = true;
                }
                else
                {
                    this.isCircuit = false;
                }

                vectorsStr = System.IO.File.ReadAllLines(path);
            }
            else
            {
                // return false if file does not exist;
                return false;
            }
            
            // convert each line into a vector3
            foreach (string vect in vectorsStr)
            {
                Vector3 vector;

                // parse the comma separated vector.
                vector.X = float.Parse(vect.Split(',')[0]);
                vector.Y = float.Parse(vect.Split(',')[1]);
                vector.Z = float.Parse(vect.Split(',')[2]);

                // if we are processing the first line in the file,
                // then we treat the vector as the starting rotation for the vehicle.
                if (isFirstLine)
                {
                    this.startingRotation = vector;
                    isFirstLine = false;
                }
                else
                {
                    checkpointPositions.Add(vector);
                }
            }

            this.checkpointManager.Load(checkpointPositions, this.isCircuit);

            // load race was successful
            return true;
        }

        private void ExitRace()
        {
            Game.Player.CanControlCharacter = true;
            this.checkpointManager.UnloadAllCheckpoints();
            this.ChangeModeTo(new InactiveMode(this.KeyPress));
        }
    }
}
