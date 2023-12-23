using Agava.YandexGames;
using UnityEngine;
using PlayerPrefs = Agava.YandexGames.PlayerPrefs;

public static class Saves
{
    public static bool IsSavesLoaded { get; private set; }

    public const string Level = nameof(Level);
    public const string IsSoundOn = nameof(IsSoundOn);
    public const string IsAutorize = nameof(IsAutorize);

    private static CloudPlayerPrefs _playerPrefs;

    static Saves()
    {
        LoadData();
    }

    public static void ClearAll()
    {
        _playerPrefs = new CloudPlayerPrefs();
        SaveData();
    }

    public static void Save<T>(string key, T saveObject)
    {
        _playerPrefs.SetString(key, JsonUtility.ToJson(saveObject));
        SaveData();
    }

    public static T Load<T>(string key, T deafultValue = default(T))
    {
        if (!_playerPrefs.HasKey(key))
            return deafultValue;

        string json = _playerPrefs.GetString(key);
        T loadObject = JsonUtility.FromJson<T>(json);

        return loadObject;
    }

    public static void Save(string key, int saveObject)
    {
        _playerPrefs.SetInt(key, saveObject);
        SaveData();
    }

    public static void Save(string key, float saveObject)
    {
        _playerPrefs.SetFloat(key, saveObject);
        SaveData();
    }

    public static void Save(string key, bool saveObject)
    {
        _playerPrefs.SetInt(key, saveObject ? 1 : 0);
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

    public static float Load(string key, float defaultValue = 0)
    {
        if (!_playerPrefs.HasKey(key))
            return defaultValue;

        return _playerPrefs.GetFloat(key);
    }

    public static bool Load(string key, bool defaultValue = false)
    {
        if (!_playerPrefs.HasKey(key))
            return defaultValue;

        return _playerPrefs.GetInt(key) > 0;
    }

    private static void SaveData()
    {
        string save = JsonUtility.ToJson(_playerPrefs);

        PlayerPrefs.SetString("Saves", save);

        PlayerAccount.SetCloudSaveData(save,
    () => Debug.Log("Success Saves!"),
    (message) => Debug.Log("Saves Error!" + message));
    }

    public static void LoadData()
    {
        PlayerAccount.GetCloudSaveData(OnSuccessLoad, OnErrorLoad);
    }

    private static void OnSuccessLoad(string json)
    {
        _playerPrefs = JsonUtility.FromJson<CloudPlayerPrefs>(json);

        IsSavesLoaded = true;
    }

    private static void OnErrorLoad(string message)
    {
        LoadFromPrefs();

        Debug.Log(message);

    }

    public static void LoadFromPrefs()
    {
        if (PlayerPrefs.HasKey("Saves"))
            _playerPrefs = JsonUtility.FromJson<CloudPlayerPrefs>(PlayerPrefs.GetString("Saves"));
        else
            _playerPrefs = new CloudPlayerPrefs();

        IsSavesLoaded = true;
    }
}
