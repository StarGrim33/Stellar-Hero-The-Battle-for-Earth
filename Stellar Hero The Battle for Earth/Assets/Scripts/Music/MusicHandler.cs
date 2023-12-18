using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] private AudioSource[] _source;
    private SoundImageHandler _handler;

    private void Awake()
    {
        _handler = GetComponent<SoundImageHandler>();
    }

    public void HandlSound()
    {
        foreach (AudioSource source in _source)
        {
            if (source.enabled)
            {
                source.enabled = false;
                _handler.ChangeImage();
            }
            else if (!source.enabled)
            {
                source.enabled = true;
                _handler.ChangeImage();
            }
        }
    }
}
