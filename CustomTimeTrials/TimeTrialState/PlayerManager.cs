using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;
using GTA.Math;

namespace CustomTimeTrials.TimeTrialState
{
    class PlayerManager
    {
        public void UnfreezePlayer()
        {
            Game.Player.CanControlCharacter = true;
        }
        public void FreezePlayer()
        {
            Game.Player.CanControlCharacter = false;
        }

        public bool isInVehicle()
        {
            return Game.Player.Character.IsSittingInVehicle();
        }

        public void MoveVehicleTo(Vector3 position, Vector3 rotation)
        {
            if (this.isInVehicle())
            {
                Vehicle vehicle = Game.Player.LastVehicle;
                vehicle.Position = position;
                vehicle.Rotation = rotation;
                GameplayCamera.RelativeHeading = 0.0f;
            }
        }
    }
}
