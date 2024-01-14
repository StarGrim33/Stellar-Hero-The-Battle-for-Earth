using Plugins.Audio.Core;
using UnityEngine;

namespace Music
{
    public class MusicHandler : MonoBehaviour, IMusicHandler
    {
        public void HandlSound()
        {
            var volume = AudioManagement.Instance.GetVolume();

            if (volume > 0.05)
            {
                AudioManagement.Instance.SetVolume(0);
            }
            else
            {
                AudioManagement.Instance.SetVolume(1);
            }
        }
    }
}