using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace CustomTimeTrials.StateMachine
{
    class StateMachine
    {
        private State state;

        public StateMachine(State initialState)
        {
            this.state = initialState;
        }

        public void onTick()
        {
            this.switchState(this.state.onTick());
        }

        public void onKeyDown(KeyEventArgs e)
        {
            this.switchState(this.state.onKeyDown(e));
        }

        public void onKeyUp(KeyEventArgs e)
        {
            this.switchState(this.state.onKeyUp(e));
        }

        private void switchState(State newState)
        {
            if (newState != null)
            {
                this.state = newState;
            }
        }
    }
}
