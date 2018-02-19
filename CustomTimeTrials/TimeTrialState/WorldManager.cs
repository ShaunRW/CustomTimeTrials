using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;
using GTA.Math;
using GTA.Native;

using CustomTimeTrials.TimeTrialData;

namespace CustomTimeTrials.TimeTrialState
{
    class WorldManager
    {
        public void SetTimeOfDay(TimeTrialData.TimeOfDay timeOfDay)
        {
            TimeSpan time;
            switch (timeOfDay)
            {
                case TimeTrialData.TimeOfDay.Midnight:
                    time = new TimeSpan(00,00,00);
                    break;
                case TimeTrialData.TimeOfDay.PreDawn:
                    time = new TimeSpan(05, 00, 00);
                    break;
                case TimeTrialData.TimeOfDay.Dawn:
                    time = new TimeSpan(05, 00, 00);
                    break;
                case TimeTrialData.TimeOfDay.Morning:
                    time = new TimeSpan(08, 00, 00);
                    break;
                case TimeTrialData.TimeOfDay.Noon:
                    time = new TimeSpan(12, 00, 00);
                    break;
                case TimeTrialData.TimeOfDay.Afternoon:
                    time = new TimeSpan(16, 00, 00);
                    break;
                case TimeTrialData.TimeOfDay.Sunset:
                    time = new TimeSpan(18, 30, 00);
                    break;
                case TimeTrialData.TimeOfDay.Dusk:
                    time = new TimeSpan(21, 00, 00);
                    break;
                default:
                    time = new TimeSpan(12,00,00);
                    break;
            }
            World.CurrentDayTime = time;
        }

        public void SetWeather(Weather weather)
        {
            World.Weather = weather;
        }

        /* Disables all traffic from spawning
         * - every now and then a rougue vehicle will spawn. Might need to do a on the fly clean up every # if frames,
         * 
         */
        public void SetTrafficOff()
        {
            // cant see vehicles from a distance at night.
            Function.Call(Hash.DISABLE_VEHICLE_DISTANTLIGHTS, true);

            // Clear current vehicles around player.
            Vector3 pos = Game.Player.Character.Position;
            Function.Call(Hash.CLEAR_AREA_OF_VEHICLES, pos.X, pos.Y, pos.Z, 10000.0f, false, false, false, false, false);

            // I guess this stops cars from spawning?
            Function.Call(Hash.SET_VEHICLE_POPULATION_BUDGET, 0);
        }

        /* This seems to work, but not 100% sure if the same density is established.
         */
        public void SetTrafficOn()
        {
            // cant see vehicles from a distance at night.
            Function.Call(Hash.DISABLE_VEHICLE_DISTANTLIGHTS, false);

            // I guess this stops cars from spawning?
            Function.Call(Hash.SET_VEHICLE_POPULATION_BUDGET, 3);
        }

        /*if (featureWorldNoTrafficUpdated)
	{
		VEHICLE::_0xF796359A959DF65D(!featureWorldNoTraffic);
		GRAPHICS::DISABLE_VEHICLE_DISTANTLIGHTS(featureWorldNoTraffic);

		if (featureWorldNoTraffic)
		{
			Vector3 v3 = ENTITY::GET_ENTITY_COORDS(PLAYER::PLAYER_PED_ID(), 1);
			GAMEPLAY::CLEAR_AREA_OF_VEHICLES(v3.x, v3.y, v3.z, 1000.0, 0, 0, 0, 0, 0);

			STREAMING::SET_VEHICLE_POPULATION_BUDGET(0);
			VEHICLE::SET_ALL_VEHICLE_GENERATORS_ACTIVE_IN_AREA(-10000.0, -10000.0, -200.0, 10000.0, 10000.0, 1000.0, 0, 1);
			PATHFIND::SET_ROADS_IN_AREA(-10000.0, -10000.0, -200.0, 10000.0, 10000.0, 1000.0, 0, 1);
		}
		else
		{
			STREAMING::SET_VEHICLE_POPULATION_BUDGET(3);
			VEHICLE::SET_ALL_VEHICLE_GENERATORS_ACTIVE();
			PATHFIND::SET_ROADS_BACK_TO_ORIGINAL(-10000.0, -10000.0, -200.0, 10000.0, 10000.0, 1000.0);
		}

		featureWorldNoTrafficUpdated = false;
	}
	else if (featureWorldNoTraffic)// && get_frame_number() % 100 == 0)
	{
		if (PED::IS_PED_IN_ANY_VEHICLE(PLAYER::PLAYER_PED_ID(), 0))
		{
			Vector3 v3 = ENTITY::GET_ENTITY_COORDS(PLAYER::PLAYER_PED_ID(), 1);
			GAMEPLAY::CLEAR_AREA_OF_VEHICLES(v3.x, v3.y, v3.z, 1000.0, 0, 0, 0, 0, 0);
		}
		STREAMING::SET_VEHICLE_POPULATION_BUDGET(0);
		VEHICLE::SET_ALL_VEHICLE_GENERATORS_ACTIVE_IN_AREA(-10000.0, -10000.0, -200.0, 10000.0, 10000.0, 1000.0, 0, 1);
		PATHFIND::SET_ROADS_IN_AREA(-10000.0, -10000.0, -200.0, 10000.0, 10000.0, 1000.0, 0, 1);
	}*/
    }
}
