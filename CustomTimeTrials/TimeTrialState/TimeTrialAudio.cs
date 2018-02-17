using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GTA;

namespace CustomTimeTrials.TimeTrialState
{
    class TimeTrialAudio
    {
        public void PlayCheckpointReachedSound()
        {
            Audio.PlaySoundFrontend("3_2_1", "HUD_MINI_GAME_SOUNDSET");
        }

        public void PlayRaceFinishedSound()
        {
            Audio.PlaySoundFrontend("RACE_PLACED", "HUD_AWARDS");
        }

        public void PlayCountdownBeep()
        {
            Audio.PlaySoundFrontend("5_SEC_WARNING", "HUD_MINI_GAME_SOUNDSET");
        }

        public void PlayRaceBeginSound()
        {
            Audio.PlaySoundFrontend("3_2_1", "HUD_MINI_GAME_SOUNDSET");
        }
    }
}
