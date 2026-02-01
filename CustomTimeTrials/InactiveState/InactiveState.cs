using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CustomTimeTrials.StateMachine;
using CustomTimeTrials.MainMenuState;


namespace CustomTimeTrials.InactiveState
{
    class InactiveState : StateMachine.State
    {
        private KeyPressTracker keyPressTracker;

        public InactiveState()
        {
            this.keyPressTracker = new KeyPressTracker();
        }

        public override State onKeyDown(KeyEventArgs e)
        {
            bool justPressed = this.keyPressTracker.update(e.KeyCode, true);

            if (justPressed && e.KeyCode == Keys.F10)
            {
                // Change State to main menu state.
                return new MainMenuState.MainMenuState();
            }

            return null;
        }
    }
}
