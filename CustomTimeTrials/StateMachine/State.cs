using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace CustomTimeTrials.StateMachine
{
    class State
    {
        public virtual State onTick() { return null; }
        public virtual State onKeyDown(KeyEventArgs e) { return null; }
        public virtual State onKeyUp(KeyEventArgs e) { return null; }
    }
}
