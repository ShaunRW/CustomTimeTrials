using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace CustomTimeTrials
{ 
    class KeyPressTracker
    {
        Dictionary<Keys, bool> KeysPressed = new Dictionary<Keys, bool>();

        public bool update(Keys Key, bool state)
        {
            if (this.KeysPressed.ContainsKey(Key))
            {
                if (this.KeysPressed[Key] != state)
                {
                    this.KeysPressed[Key] = state;
                    return true;
                }
            }
            else
            {
                this.KeysPressed.Add(Key, state);
                return true;
            }
            return false;
        }
    }
}
