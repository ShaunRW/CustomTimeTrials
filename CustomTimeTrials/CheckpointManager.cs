using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA.Math;

namespace CustomTimeTrials
{
    enum CheckpointManagerEvent
    {
        None,
        TargetReached,
        LapEndReached
    }

    class CheckpointManager
    {
        private List<Checkpoint> checkpoints = new List<Checkpoint>();

        private int targetIndex;
        private int lapEndIndex;
        private bool isCircuit
        {
            get { return lapEndIndex == 0; }
        }

        private CheckpointColor checkpointColor = new CheckpointColor(255, 255, 0, 100);
        private CheckpointColor checkpointIconColor = new CheckpointColor(0, 0, 255, 75);
        private float checkpointRadius = 15.0f;
        private float checkpointHeight = 6.0f;



        public CheckpointManager()
        {
            this.targetIndex = 1;
        }


        public Checkpoint GetByIndex(int index)
        {
            if (index < this.checkpoints.Count)
            {
                return this.checkpoints[index];
            }
            else
            {
                return null;
            }
        }

        public int NextTargetIndex()
        {
            return this.IndexAfter(this.targetIndex);
        }

        private int IndexAfter(int afterIndex)
        {
            int next = afterIndex + 1;
            if (next == this.checkpoints.Count)
            {
                next = 0;
            }
            return next;
        }


        private CheckpointIcon GetNextIcon(bool isLastLap)
        {
            if (this.IsFinishLine(this.NextTargetIndex(), isLastLap))
            {
                return CheckpointIcon.Checker;
            }
            else
            {
                return CheckpointIcon.Arrow;
            }
        }

        private bool IsFinishLine(int index, bool isLastLap)
        {
            if (index == this.lapEndIndex)
            {
                if ((this.isCircuit && isLastLap) || (!this.isCircuit))
                {
                    return true;
                }
            }
            return false;
        }

        public void TargetNext(bool isLastLap = false)
        {
            CheckpointIcon icon = this.GetNextIcon(isLastLap);

            // Target the next index
            this.targetIndex = this.NextTargetIndex();

            // display the next checkpoint.
            this.Show(this.targetIndex, icon);

            if (!this.IsFinishLine(this.targetIndex, isLastLap))
            {
                this.ShowFutureBlip();
            }
        }


        public void Show(int index, CheckpointIcon Icon)
        {
            this.GetByIndex(index).create_gta_checkpoint_beacon(Icon, this.checkpointColor, this.checkpointIconColor, this.checkpointRadius, this.checkpointHeight);
            this.GetByIndex(index).CreateGTABlip();
        }

        public void Hide(int index)
        {
            this.GetByIndex(index).delete_gta_checkpoint_beacon();
            this.GetByIndex(index).DeleteGTABlip();
        }

        public void ShowFutureBlip(float scale=0.5f)
        {
            this.GetByIndex(this.NextTargetIndex()).CreateGTABlip(scale);
        }

        public CheckpointManagerEvent GetEvent()
        {
            CheckpointManagerEvent _event = CheckpointManagerEvent.None; 

            if (this.checkpoints.Count > this.targetIndex)
            {
                if (this.GetByIndex(this.targetIndex).isPlayerNear())
                {
                    if (this.targetIndex == this.lapEndIndex)
                    {
                        _event = CheckpointManagerEvent.LapEndReached;
                    }
                    else
                    {
                        _event = CheckpointManagerEvent.TargetReached;
                    }
                    this.Hide(this.targetIndex);
                }
            }

            return _event;
        }


        public void Load(List<Vector3> positions, bool isCircuit)
        {
            Vector3 PointTo;

            for (int i = 0; i < positions.Count; i++)
            {
                if (i == positions.Count - 1)
                {
                    PointTo = positions[0];
                }
                else
                {
                    PointTo = positions[i + 1];
                }
                this.checkpoints.Add(new Checkpoint(positions[i], PointTo));
            }

            if (isCircuit)
            {
                this.lapEndIndex = 0;
            }
            else
            {
                this.lapEndIndex = this.checkpoints.Count - 1;
            }
        }


        public void UnloadAllCheckpoints()
        {
            for (int i=0; i<this.checkpoints.Count; i++)
            {
                this.Hide(i);
            }
            this.checkpoints.Clear();
        }

    }
}
