using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;
using GTA.Native;
using GTA.Math;

namespace CustomTimeTrials.TimeTrialState
{
    class CheckpointColor
    {
        public int red;
        public int green;
        public int blue;
        public int alpha;

        public CheckpointColor(int red, int green, int blue, int alpha=255)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }
    }

    enum CheckpointIcon
    {
        Arrow = 0,
        Checker = 4,
        None = 45
    }

    class Checkpoint
    {
        private int checkpoint_handle;
        private bool isVisible;
        public Vector3 position;
        public Vector3 pointTo;
        private float positionZOffset = 8.0f;

        private Blip blipHandle;
        private bool blipVisible;


        public Checkpoint(Vector3 position, Vector3 PointTo)
        {
            this.position = position;
            this.pointTo = PointTo;
        }

        public void create_gta_checkpoint_beacon(CheckpointIcon iconType, CheckpointColor color, CheckpointColor iconColor, float radius, float height)
        {
            if (this.isVisible == false)
            {
                this.isVisible = true;

                this.checkpoint_handle = Function.Call<int>(Hash.CREATE_CHECKPOINT, (int)iconType, this.position.X, this.position.Y, this.position.Z-this.positionZOffset, this.pointTo.X, this.pointTo.Y, this.pointTo.Z, radius, color.red, color.green, color.blue, color.alpha, 0);
                Function.Call(Hash._SET_CHECKPOINT_ICON_RGBA, this.checkpoint_handle, iconColor.red, iconColor.green, iconColor.blue, iconColor.alpha);
                Function.Call(Hash.SET_CHECKPOINT_CYLINDER_HEIGHT, this.checkpoint_handle, height+this.positionZOffset, height+this.positionZOffset, height);
            }
        }

        public void delete_gta_checkpoint_beacon()
        {
            if (this.isVisible)
            {
                this.isVisible = false;
                Function.Call(Hash.DELETE_CHECKPOINT, this.checkpoint_handle);
            }
        }

        public void CreateGTABlip(float scale=1.0f)
        {
            if (!this.blipVisible)
            {
                this.blipHandle = Function.Call<Blip>(Hash.ADD_BLIP_FOR_COORD, this.position.X, this.position.Y, this.position.Z);
                this.blipHandle.IsShortRange = true;
                this.blipVisible = true;
            }
            this.blipHandle.Scale = scale;
        }

        public void DeleteGTABlip()
        {
            if (this.blipVisible)
            {
                this.blipHandle.Remove();
                this.blipVisible = false;
            }
        }


        public override string ToString()
        {
            return this.position.X.ToString("G") + "," + this.position.Y.ToString("G") + "," + this.position.Z.ToString("G");
        }

        public bool isPlayerNear()
        {
            return Game.Player.Character.IsInRangeOf(this.position, 15.0f);
        }
    }
}
