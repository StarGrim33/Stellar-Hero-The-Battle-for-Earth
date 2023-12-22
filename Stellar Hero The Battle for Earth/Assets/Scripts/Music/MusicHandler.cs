using Plugins.Audio.Core;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private SourceAudio[] _source;
    [SerializeField] private AudioPauseHandler _sourceGlobal;
    private SoundImageHandler _handler;

    private void Awake()
    {
        _handler = GetComponent<SoundImageHandler>();
    }

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


        //foreach (SourceAudio source in _source)
        //{
        //    if (!source.Mute)
        //    {
        //        source.Mute = true;
        //        _handler.ChangeImage();
        //    }
        //    else if (source.Mute)
        //    {
        //        source.Mute = false;
        //        _handler.ChangeImage();
        //    }
        //}
    }
}
