using System;
using TMPro;
using UnityEngine;

public class CurrentWaveView : MonoBehaviour
{
   [SerializeField] private TMP_Text _text;
    private Spawner _spawner;

    public void Init(Spawner spawner)
    {
        _spawner = spawner;
        _spawner.WaveChanged += WaveChanged;
    }

    private void OnDisable()
    {
        _spawner.WaveChanged -= WaveChanged;
    }

    private void Start()
    {
        if( _spawner == null )
            throw new Exception("Check spawner and enemy pool for available space");
    }

    private void WaveChanged(int value)
    {
        value++;
        _text.text = value.ToString();
    }
}
