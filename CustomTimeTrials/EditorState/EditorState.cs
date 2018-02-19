using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using GTA;
using GTA.Math;

using CustomTimeTrials.StateMachine;
using CustomTimeTrials.TimeTrialData;

namespace CustomTimeTrials.EditorState
{
    class EditorState : StateMachine.State
    {
        // untilitiy properties
        private State newState;
        private GUI.EditorMenu editorMenu = new GUI.EditorMenu();

        // data
        private TimeTrialData.TimeTrialSaveData data = new TimeTrialData.TimeTrialSaveData();


        public EditorState()
        {
            this.editorMenu.CreateMenu(this.onMenuExit, this.onAddCheckpoint, this.onRemoveLastCheckpoint, this.onRemoveAllCheckpoints, this.onSaveTimeTrial);   
        }

        public override State onTick()
        {
            this.editorMenu.UpdateMenu();
            this.DrawCheckpointMarkers();
            return this.newState;
        }

        private void DrawCheckpointMarkers()
        {
            Vector3 emptyVector = new Vector3(0, 0, 0);
            foreach (SimpleVector3 cp in this.data.checkpoints)
            {
                World.DrawMarker(MarkerType.VerticalCylinder, cp.ToGtaVector3(), emptyVector, emptyVector, new Vector3(1, 1, 1), System.Drawing.Color.Red);
            }
        }

        /* MENU EVENT CALLBACKS 
        ======================== */
        private void onAddCheckpoint()
        {
            // Get the players position at ground level
            Vector3 position = Game.Player.Character.Position;
            position.Z -= Game.Player.Character.HeightAboveGround;

            // if this is the first checkpoint, set the starting data.
            if (this.data.checkpoints.Count == 0)
            {
                this.data.start.rotation = new SimpleVector3(Game.Player.Character.Rotation);
                this.data.start.position = new SimpleVector3(position);
            }

            // add the checkpoint position to the checkpoint list.
            this.data.checkpoints.Add(new SimpleVector3(position));
        }

        private void onRemoveLastCheckpoint()
        {
            int last_index = this.data.checkpoints.Count - 1;
            if (last_index > -1)
            {
                this.data.checkpoints.RemoveAt(last_index);
            }
        }

        private void onRemoveAllCheckpoints()
        {
            this.data.checkpoints.Clear();
        }

        private void onSaveTimeTrial()
        {
            this.data.displayName = Game.GetUserInput("Untitled Time Trial", 40);
            this.data.type = this.editorMenu.GetRaceType().ToLower();

            // Put the data in a file object and save it
            TimeTrialFile newFile = new TimeTrialData.TimeTrialFile();
            newFile.data = this.data;
            newFile.save();
        }

        private void onMenuExit()
        {
            this.newState = new InactiveState.InactiveState();
        }
    }
}
