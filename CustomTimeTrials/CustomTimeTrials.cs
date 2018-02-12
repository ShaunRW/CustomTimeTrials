using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GTA;
using GTA.Native;
using GTA.Math;

namespace CustomTimeTrials
{
    public class CustomTimeTrials : Script
    {

        private KeyPressTracker keyPressTracker = new KeyPressTracker();
        private IMode mode;


        public CustomTimeTrials()
        {
            UI.Notify("Custom Time Trials 1.0 by ShaunRW!");

            this.mode = new InactiveMode(this.keyPressTracker);

            this.Tick += onTick;
            this.KeyDown += onKeyDown;
            this.KeyUp += onKeyUp;
        }

        private void onTick(object sender, EventArgs e)
        {
            this.mode.onTick();
            this.mode = this.mode.GetNewMode();
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            this.mode.onKeyDown(e);
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            this.mode.onKeyUp(e);
        }
    }
}
