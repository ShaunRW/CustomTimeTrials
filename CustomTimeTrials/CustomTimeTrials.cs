using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GTA;
using GTA.Native;
using GTA.Math;

using CustomTimeTrials.StateMachine;
using CustomTimeTrials.InactiveState;

namespace CustomTimeTrials
{
    public class CustomTimeTrials : Script
    {

        private StateMachine.StateMachine stateMachine;

        public CustomTimeTrials()
        {
            UI.Notify(string.Format("Custom Time Trials {0} by ShaunRW!", this.GetFriendlyVersionNumber()));

            this.stateMachine = new StateMachine.StateMachine(new InactiveState.InactiveState());

            this.Tick += onTick;
            this.KeyDown += onKeyDown;
            this.KeyUp += onKeyUp;
        }

        private void onTick(object sender, EventArgs e)
        {
            this.stateMachine.onTick();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            this.stateMachine.onKeyDown(e);
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            this.stateMachine.onKeyUp(e);
        }

        private string GetFriendlyVersionNumber()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return string.Format("v{0}.{1}.{2}", version.Major, version.Minor, version.Build);
        }
    }
}
