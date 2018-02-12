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
    class EditorMode : Mode
    {

        Vector3 startingRotation;
        List<Checkpoint> checkpoints = new List<Checkpoint>();
        EditorMenu menu;

        public EditorMode(KeyPressTracker keyPressTracker) : base(keyPressTracker) {
            Game.Player.CanControlCharacter = true;
            this.menu = new EditorMenu();
            this.menu.SetCallback("Add Checkpoint Here", this.AddCheckpointHere);
            this.menu.SetCallback("Remove Last Checkpoint", this.RemoveLastCheckpoint);
            this.menu.SetCallback("Remove All Checkpoints", this.RemoveAllCheckpoints);
            this.menu.SetCallback("Save Time Trial", this.SaveRace);
            this.menu.SetCallback("Exit Editor", this.ExitEditor);
        }

        public override void onTick()
        {
            this.menu.update();
        }

        private void AddCheckpointHere()
        {
            if (this.checkpoints.Count == 0)
            {
                this.startingRotation = Game.Player.Character.LastVehicle.Rotation;
            }

            Vector3 pos = Game.Player.Character.Position;
            pos.Z -= Game.Player.Character.HeightAboveGround; 

            var color = new CheckpointColor(255, 0, 0);

            Checkpoint cp = new Checkpoint(pos, pos);
            cp.create_gta_checkpoint_beacon(CheckpointIcon.None, color, color, 1.0f, 4.0f);

            this.checkpoints.Add(cp);
        }

        private void RemoveAllCheckpoints()
        {
            foreach (Checkpoint cp in this.checkpoints)
            {
                cp.delete_gta_checkpoint_beacon();
            }
            checkpoints.Clear();
        }

        private void RemoveLastCheckpoint()
        {
            int last_index = this.checkpoints.Count - 1;
            if (last_index > -1)
            {
                this.checkpoints[last_index].delete_gta_checkpoint_beacon();
                this.checkpoints.RemoveAt(last_index);
            }
        }

        private void SaveRace()
        {
            string name = Game.GetUserInput(40);
            UI.Notify("Saving "+name);

            List<string> positions = new List<string>();

            // since the rotation and position are both saved as vector3 csv strings just pretend the rot is a pos. 
            var rotation = new Checkpoint(this.startingRotation, this.startingRotation);
            positions.Add(rotation.ToString());

            foreach(Checkpoint cp in checkpoints)
            {
                positions.Add(cp.ToString());
            }

            string ext = "." + this.menu.GetSelectedType().ToLower();

            string path = "scripts/TimeTrials/" + name + ext;
            System.IO.File.WriteAllLines(path, positions.ToArray());
        }

        private void ExitEditor()
        {
            this.RemoveAllCheckpoints();
            this.ChangeModeTo(new InactiveMode(this.KeyPress));
        }
    }
}
