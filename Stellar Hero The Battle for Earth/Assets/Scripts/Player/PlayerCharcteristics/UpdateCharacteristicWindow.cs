using System.Collections.Generic;
using UnityEngine;

public class UpdateCharacteristicWindow : MonoBehaviour
{
    [SerializeField] private List<CharacteristicChangerConfig> _characteristicChangers;
    [SerializeField] private int _chengersCount = 3;

    [SerializeField] private CharacteristicChanger _characteristicChangerTemplate;

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    public void Init()
    {
        _characteristicChangers.Shuffle();

        for (int i =0; i< _chengersCount; i++)
        {
            var template = Instantiate(_characteristicChangerTemplate, gameObject.transform);
            template.Init(_characteristicChangers[i]);
        }
    }
}
