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

        public void CantDie(bool cantDie=true)
        {
            bool isTrue = cantDie;
            bool isFalse = !cantDie;

            /* Doesn't hurt player when:
             * - Getting shot
             */
            Game.Player.Character.IsBulletProof = isTrue; // works

            /* Doesn't hurt player when:
             * - Is near explosion
             * - Is in exploding vehicle.
             */
            Game.Player.Character.IsExplosionProof = isTrue; // works

            /* Player can't catch on fire. */
            Game.Player.Character.IsFireProof = isTrue; // works

            /* Doesn't hurt player when:
             * - Hit by vehicle.
             * - is ragdolling
             * - flung off motorbike.
             */
            Game.Player.Character.IsCollisionProof = isTrue;

            /* Doesn't hurt player when:
             * - is getting attacked with a melee weapon or punches
             * - is getting attacked by animal, except when underwater.
             */
            Game.Player.Character.IsMeleeProof = isTrue;

            /* Stops the player from flying through the windscreen.
             * Although it still hurts the player even when isCollisionProof
             */
            Game.Player.Character.CanFlyThroughWindscreen = isFalse;

            /* Stops the player from getting dragged from a vehicle or bike.
             */
            Game.Player.Character.CanBeDraggedOutOfVehicle = isFalse;


            /* Stops the players breath from decreasing while swimming under water.
             * - Doesn't work while in a vehicle.
             */
            Game.Player.Character.DrownsInWater = isFalse;

            /* Stops the players breath from decreasing while sitting in a sinking vehicle.
             * - Doesn't work while swimming underwater.
             */
            Game.Player.Character.DrownsInSinkingVehicle = isFalse;
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
