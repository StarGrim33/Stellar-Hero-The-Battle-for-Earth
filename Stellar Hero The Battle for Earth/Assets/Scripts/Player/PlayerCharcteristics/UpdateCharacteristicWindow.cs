using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCharacteristicWindow : MonoBehaviour
{
    [SerializeField] private List<CharacteristicChangerConfig> _characteristicChangers;
    [SerializeField] private CharacteristicChanger _characteristicChangerTemplate;
    [SerializeField] private Button _menuButton;
    [SerializeField] private int _chengersCount = 3;

    private List<CharacteristicChanger> _currentChangers = new List<CharacteristicChanger>();

    private void Start()
    {
        Init();
    }

    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        StateManager.Instance.SetState(GameStates.Paused);
        StateManager.Instance.IsLevelUpPanelShowing = true;
        Debug.Log(StateManager.Instance.IsLevelUpPanelShowing);
        _menuButton.enabled = false;
    }

    private void OnDisable()
    {
        StateManager.Instance.SetState(GameStates.Gameplay);
        StateManager.Instance.IsLevelUpPanelShowing = false;
        Debug.Log(StateManager.Instance.IsLevelUpPanelShowing);
        _menuButton.enabled = transform;
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
