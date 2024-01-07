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

    private void OnDisable() => _spawner.WaveChanged -= WaveChanged;

    private void WaveChanged(int value) => _text.text = value.ToString();
}
