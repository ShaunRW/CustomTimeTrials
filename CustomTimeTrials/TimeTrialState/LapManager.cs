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

        private TimeManager lapTimer = new TimeManager();
        public int currentLapTime
        {
            get { return this.lapTimer.elapsed; }
        }
        public int fastestLapTime { get; private set; }

        public LapManager(int lapCount, string raceType, Action onNewLapCallback, Action onRaceFinishedCallback)
        {
            this.count = lapCount;
            this.current = 0;
            this.type = raceType;

            this.onNewLapCallback = onNewLapCallback;
            this.onRaceFinishedCallback = onRaceFinishedCallback;
        }

        public void AddLap()
        {
            this.UpdateFastestLapTime();
            this.lapTimer.Reset();

            this.current += 1;

            this.onNewLapCallback();
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
                    this.AddLap();
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
                int showLap = (this.current != 0) ? this.current : 1; 
                return string.Format("{0}/{1}", showLap, this.count);
            }
            else
            {
                return "N/A";
            }
        }

        public void UpdateFastestLapTime()
        {
            if (this.current == 1 || this.currentLapTime < this.fastestLapTime)
            {
                this.fastestLapTime = this.currentLapTime;
            }
        }
    }
}
