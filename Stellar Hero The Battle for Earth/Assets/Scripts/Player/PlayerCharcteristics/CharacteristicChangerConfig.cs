using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Player
{
    [CreateAssetMenu(menuName = "Source/Updates", fileName = "UpdasteConfig", order = 0)]
    public class CharacteristicChangerConfig : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _enDescription;
        [SerializeField] private string _ruDescription;
        [SerializeField] private string _trDescription;
        [SerializeField] private List<Changes> _changes;
        [SerializeField] private CharacteristicChangerConfig _nextChanger;

        public Sprite Icon => _icon;

        public CharacteristicChangerConfig NextChanger => _nextChanger;

        public string GetDescription()
        {
            string lang = Language.Instance.CurrentLanguage;

            return lang switch
            {
                Constants.EnglishCode => _enDescription,
                Constants.RussianCode => _ruDescription,
                Constants.TurkishCode => _trDescription,
                _ => _enDescription,
            };
        }

        public void ChangeCharacteristic()
        {
            foreach (var changes in _changes)
            {
                PlayerCharacteristics.I.AddValue(changes.Characteristics, changes.Value);
            }
        }
    }
}