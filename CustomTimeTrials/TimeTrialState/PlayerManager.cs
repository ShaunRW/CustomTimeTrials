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
        private Vehicle vehicle;

        public PlayerManager()
        {
            if(this.isInVehicle())
            {
                this.vehicle = Game.Player.LastVehicle;
            }
        }

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
                this.vehicle.Position = position;
                this.vehicle.Rotation = rotation;
                GameplayCamera.RelativeHeading = 0.0f;
            }
        }

        public void SetVehicleInvincible()
        {
            this.vehicle.CanBeVisiblyDamaged = false;
            this.vehicle.CanTiresBurst = false;
            this.vehicle.CanWheelsBreak = false;
            this.vehicle.EngineCanDegrade = false;
            this.vehicle.IsBulletProof = true;
            this.vehicle.IsCollisionProof = true;
            this.vehicle.IsExplosionProof = true;
            this.vehicle.IsInvincible = true;
            this.vehicle.IsMeleeProof = true;
        }
    }
}
