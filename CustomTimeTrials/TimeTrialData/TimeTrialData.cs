using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA.Math;

namespace CustomTimeTrials.TimeTrialData
{
    class SimpleVector3
    {
        public float X;
        public float Y;
        public float Z;

        public SimpleVector3(Vector3 gtaVector3)
        {
            this.X = gtaVector3.X;
            this.Y = gtaVector3.Y;
            this.Z = gtaVector3.Z;
        }

        public Vector3 ToGtaVector3()
        {
            return new Vector3(this.X, this.Y, this.Z);
        }
    }

    struct TimeTrialStartData
    {
        public SimpleVector3 position;
        public SimpleVector3 rotation;
    }

    class TimeTrialSaveData
    {
        public string displayName;
        public string type;
        public TimeTrialStartData start;
        public List<SimpleVector3> checkpoints = new List<SimpleVector3>();
    }
}
