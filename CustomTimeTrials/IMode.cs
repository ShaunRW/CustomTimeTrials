using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CustomTimeTrials
{
    interface IMode
    {
        void onTick();
        void onKeyDown(KeyEventArgs e);
        void onKeyUp(KeyEventArgs e);
        IMode GetNewMode();
    }
}
