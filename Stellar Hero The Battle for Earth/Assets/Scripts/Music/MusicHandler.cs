using Plugins.Audio.Core;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private SourceAudio[] _source;

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
