using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;

namespace CustomTimeTrials.TimeTrialData
{
    enum TimeOfDay
    {
        Midnight,
        PreDawn,
        Dawn,
        Morning,
        Noon,
        Afternoon,
        Sunset,
        Dusk
    }
    class SetupData
    {
        public int lapCount;
        public TimeOfDay timeOfDay;
        public Weather weather;
        public bool vehicleDamageOn;
    }
}
