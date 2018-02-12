using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace CustomTimeTrials
{
    class InactiveMode : Mode
    {

        public InactiveMode(KeyPressTracker keyPressTracker) : base(keyPressTracker)
        {
            
        }

        public override void onKeyDown(KeyEventArgs e)
        {
            bool justPressed = this.KeyPress.update(e.KeyCode, true);

            if (justPressed && e.KeyCode == Keys.F9)
            {
                //this.ChangeModeTo(new RaceMode("test", 1, this.KeyPress));
                this.ChangeModeTo(new RaceSelectMode(this.KeyPress));
            }
        }
    }
}
