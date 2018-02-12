using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace CustomTimeTrials
{
    abstract class Mode : IMode
    {

        protected KeyPressTracker KeyPress;
        private bool doChangeMode = false;
        private IMode changeModeTo;

        public Mode(KeyPressTracker keyPressTracker)
        {
            this.KeyPress = keyPressTracker;
        }

        public virtual void onTick()
        {
            // do nothing here.
        }

        public virtual void onKeyDown(KeyEventArgs e)
        {
            bool justPressed = this.KeyPress.update(e.KeyCode, true);
        }

        public virtual void onKeyUp(KeyEventArgs e)
        {
            bool justReleased = this.KeyPress.update(e.KeyCode, false);
        }

        protected void ChangeModeTo(IMode newMode)
        {
            this.doChangeMode = true;
            this.changeModeTo = newMode;
        }

        public IMode GetNewMode()
        {
            if (this.doChangeMode)
            {
                return this.changeModeTo;
            }
            else
            {
                return this;
            }
        }
    }
}
