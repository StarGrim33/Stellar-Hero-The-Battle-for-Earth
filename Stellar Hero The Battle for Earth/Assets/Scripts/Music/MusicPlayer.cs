using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private List<AudioClip> _clips;
    private int _currentClipIndex = 0;

    private void Start()
    {
        PlayNextClip();
    }

    private void PlayNextClip()
    {
        if (_currentClipIndex < _clips.Count)
        {
            _source.clip = _clips[_currentClipIndex];
            _source.Play();
            _currentClipIndex++;
        }
        else
        {
            _currentClipIndex = 0;
        }
    }

    public void Pause()
    {
        _source.Pause();
    }

    public void UnPause()
    {
        _source.UnPause();
    }

    public void ChangeVolume(float value)
    {
        _source.volume = value;
    }
}
