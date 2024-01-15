using Agava.YandexGames;
using UnityEngine;
using Utils;
using PlayerPrefs = Agava.YandexGames.PlayerPrefs;

namespace SDK
{
    public static class Saves
    {
        public const string Level = nameof(Level);
        public const string IsSoundOn = nameof(IsSoundOn);
        public const string IsAutorize = nameof(IsAutorize);

        private static CloudPlayerPrefs _playerPrefs;

        public static bool IsSavesLoaded { get; private set; }

        static Saves()
        {
            LoadData();
        }

        public static void Save(string key, int saveObject)
        {
            _playerPrefs.SetInt(key, saveObject);
            SaveData();
        }

        public static int Load(string key, int defaultValue = 0)
        {
            if (!_playerPrefs.HasKey(key))
            {
                return defaultValue;
            }

            return _playerPrefs.GetInt(key);
        }

        public static void LoadFromPrefs()
        {
            if (PlayerPrefs.HasKey(Constants.Saves))
            {
                _playerPrefs = JsonUtility.FromJson<CloudPlayerPrefs>(PlayerPrefs.GetString(Constants.Saves));
            }
            else
            {
                _playerPrefs = new CloudPlayerPrefs();
            }

            IsSavesLoaded = true;
        }

        public static void LoadData()
        {
            PlayerAccount.GetCloudSaveData(OnSuccessLoad, OnErrorLoad);
        }

        private static void SaveData()
        {
            string save = JsonUtility.ToJson(_playerPrefs);
            PlayerPrefs.SetString(Constants.Saves, save);
            PlayerAccount.SetCloudSaveData(save);
        }

        private static void OnSuccessLoad(string json)
        {
            _playerPrefs = JsonUtility.FromJson<CloudPlayerPrefs>(json);
            IsSavesLoaded = true;
        }

        private static void OnErrorLoad(string message)
        {
            LoadFromPrefs();
        }
    }
}