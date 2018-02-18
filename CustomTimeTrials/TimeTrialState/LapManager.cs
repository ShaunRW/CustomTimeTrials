using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomTimeTrials.TimeTrialState
{
    class LapManager
    {
        private string type;
        public bool isCircuit
        {
            get { return this.type == "circuit"; }
        }

        public int count;
        public int current;
        public bool onLast
        {
            get { return (this.isCircuit) ? this.current == this.count : true; }
        }

        private Action onRaceFinishedCallback;
        private Action onNewLapCallback; 

        public LapManager(int lapCount, string raceType, Action onNewLapCallback, Action onRaceFinishedCallback)
        {
            this.count = lapCount;
            this.current = 1;
            this.type = raceType;

            this.onNewLapCallback = onNewLapCallback;
            this.onRaceFinishedCallback = onRaceFinishedCallback;
        }

        public void EndCurrentLap()
        {
            if (this.isCircuit)
            {
                if (this.onLast)
                {
                    this.onRaceFinishedCallback();
                }
                else
                {
                    this.current += 1;
                    this.onNewLapCallback();
                }
            }
            else
            {
                this.onRaceFinishedCallback();
            }
        }

        public override string ToString()
        {
            if (this.isCircuit)
            {
                return string.Format("{0}/{1}", this.current, this.count);
            }
            else
            {
                return "N/A";
            }
        }
    }
}
