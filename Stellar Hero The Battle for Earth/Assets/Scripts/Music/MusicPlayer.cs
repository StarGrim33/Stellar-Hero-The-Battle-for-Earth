using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private List<AudioClip> _clips;
    private int _currentClipIndex = 0;

    private void Start()
    {
        StartCoroutine(NextTrack());
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

    private IEnumerator NextTrack()
    {
        while(_clips.Count > 0)
        {
            if (_currentClipIndex < _clips.Count)
            {
                _source.clip = _clips[_currentClipIndex];
                _source.Play();

                yield return new WaitForSeconds(_source.clip.length);

                _currentClipIndex++;
            }
            else
            {
                _currentClipIndex = 0;
            }
        }
    }
}
