using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA.Math;

using GTA;

namespace CustomTimeTrials.TimeTrialState
{
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

        private Action checkpointReachedCallback;
        private Action lapCompleteCallback;

        public CheckpointManager(Action checkpointReachedCallback, Action lapCompleteCallback)
        {
            this.targetIndex = 1;

            this.checkpointReachedCallback = checkpointReachedCallback;
            this.lapCompleteCallback = lapCompleteCallback;
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

        public void ShowFutureBlip(float scale = 0.5f)
        {
            this.GetByIndex(this.NextTargetIndex()).CreateGTABlip(scale);
        }

        public void Update(bool isOnLastLap)
        {

            if (this.checkpoints.Count > this.targetIndex)
            {
                if (this.GetByIndex(this.targetIndex).isPlayerNear())
                {
                    this.Hide(this.targetIndex);
                    if (this.targetIndex == this.lapEndIndex)
                    {
                        this.lapCompleteCallback();
                        if (!isOnLastLap)
                        {
                            this.TargetNext(isOnLastLap);
                        }
                    }
                    else
                    {
                        this.checkpointReachedCallback();
                        this.TargetNext(isOnLastLap);
                    }
                }
            }
        }


        public void Load(TimeTrialData data)
        {
            Vector3 PointTo;

            for (int i = 0; i < data.checkpoints.Count; i++)
            {
                if (i == data.checkpoints.Count - 1)
                {
                    PointTo = data.checkpoints[0].ToGtaVector3();
                }
                else
                {
                    PointTo = data.checkpoints[i + 1].ToGtaVector3();
                }
                this.checkpoints.Add(new Checkpoint(data.checkpoints[i].ToGtaVector3(), PointTo));
            }

            if (data.type == "circuit")
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
            for (int i = 0; i < this.checkpoints.Count; i++)
            {
                this.Hide(i);
            }
            this.checkpoints.Clear();
        }

    }
}