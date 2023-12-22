using Plugins.Audio.Core;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private SourceAudio _source;

    private void Start()
    {
        _source.Play("Battle");
    }

    public void Pause()
    {
        _source.Mute = true;    
    }

    public void UnPause()
    {
        _source.Mute = false;
    }
}
