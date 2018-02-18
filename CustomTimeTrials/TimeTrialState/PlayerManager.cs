using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;
using GTA.Math;
using GTA.Native;

namespace CustomTimeTrials.TimeTrialState
{
    class PlayerManager
    {
        private Vehicle vehicle;

        /*
         * Player Methods
         */
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

        private bool PlayerIsDamaged()
        {
            if (Game.Player.Character.Health < 100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void HealPlayer()
        {
            Game.Player.Character.Health = 100;
        }

        public void HealPlayerIfDamaged()
        {
            if (this.PlayerIsDamaged())
            {
                this.HealPlayer();
            }
        }

        public bool isInVehicle()
        {
            return Game.Player.Character.IsSittingInVehicle();
        }


        /*
         * Vehicle Methods
         */
        public void MoveVehicleTo(Vector3 position, Vector3 rotation)
        {
            if (this.isInVehicle())
            {
                this.vehicle.Position = position;
                this.vehicle.Rotation = rotation;
                GameplayCamera.RelativeHeading = 0.0f;
            }
        }

        public void SetVehicleDamageOn(bool damage=true)
        {

            bool _true = !damage;
            bool _false = damage;

            // SET PROOFS

            /* Stops the vehicle from being damaged by bullets
             * - engine will not be damanged
             * - panel work will not be damaged
             * - lights will not be damanged
             * - windows WILL be damanged
             * - Bullet holes do show up.
             */
            this.vehicle.IsBulletProof = _true;

            /* Untested
             */
            this.vehicle.IsFireProof = _true;

            /* this makes the car not get damaged by explosions
             */
            this.vehicle.IsExplosionProof = _true;

            /* This stops panels from detaching from crashes
             * - Everything else still deforms.
             * - Tire can get stuck from deform.
             */
            this.vehicle.IsCollisionProof = _true;

            /* this MIGHT not lose health on melee hits;
             * - Everything else still deforms and smashes.
             */
            this.vehicle.IsMeleeProof = _true;



            /* stops the vehicle tires from bursting:
             * - While doing a burnout
             * - While being shot.
             * - not tested on spike strips yet.
             */
            this.vehicle.CanTiresBurst = _false;

            /* Untested
             */
            this.vehicle.CanWheelsBreak = _false;

            /* Untested
             */
            this.vehicle.EngineCanDegrade = _false;
           
            /* Not sure what this does.
             */
            this.vehicle.CanBeVisiblyDamaged = _false;

            this.vehicle.IsOnlyDamagedByPlayer = _true;

            /* Sets doors unbreakable.
             */ 
            this.vehicle.SetDoorBreakable(VehicleDoor.FrontLeftDoor, _false);
            this.vehicle.SetDoorBreakable(VehicleDoor.FrontRightDoor, _false);
            this.vehicle.SetDoorBreakable(VehicleDoor.BackLeftDoor, _false);
            this.vehicle.SetDoorBreakable(VehicleDoor.BackRightDoor, _false);
            this.vehicle.SetDoorBreakable(VehicleDoor.Hood, _false);
            this.vehicle.SetDoorBreakable(VehicleDoor.Trunk, _false);
        }

        public void FixVehicleIfDamaged()
        {
            if (this.vehicle.IsDamaged)
            {
                this.vehicle.Repair();
            }
        }


    }
}
