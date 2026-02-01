using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomTimeTrials.RecordData
{
    struct CurrentRecord
    {
        public float fastestTime;
        public float fastestLapTime;
    }

    struct LapRaceRecords
    {
        public float fastestTime;
    }

    struct RaceRecords
    {
        public float fastestLapTime;
        public Dictionary<int, LapRaceRecords> fastestTimes;
    }

    struct RecordData
    {
        public Dictionary<string, RaceRecords> records;
    }
}
