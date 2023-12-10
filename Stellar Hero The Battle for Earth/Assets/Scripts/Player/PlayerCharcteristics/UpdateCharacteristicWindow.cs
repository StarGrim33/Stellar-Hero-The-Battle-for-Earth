using System.Collections.Generic;
using UnityEngine;

public class UpdateCharacteristicWindow : MonoBehaviour
{
    [SerializeField] private List<CharacteristicChangerConfig> _characteristicChangers;
    [SerializeField] private int _chengersCount = 3;

    [SerializeField] private CharacteristicChanger _characteristicChangerTemplate;

    private List<CharacteristicChanger> _currentChangers = new List<CharacteristicChanger>();

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
    }

    public void CharacteristicChangerListUpdated(CharacteristicChangerConfig changer)
    {
        if (changer.NextChanger != null)
            _characteristicChangers.Add(changer.NextChanger);

        _characteristicChangers.Remove(changer);

        RemoveChangers();

        Init();
    }

    private void Init()
    {
        _characteristicChangers.Shuffle();

        _currentChangers.Clear();

        _chengersCount = Mathf.Min(_chengersCount, _characteristicChangers.Count);

        for (int i = 0; i < _chengersCount; i++)
        {
            var template = Instantiate(_characteristicChangerTemplate, gameObject.transform);
            template.Init(_characteristicChangers[i]);

            _currentChangers.Add(template);
        }
    }

    private void RemoveChangers()
    {
        foreach(var changer in _currentChangers)
        {
            Destroy(changer.gameObject);
        }
    }
}
