using System.Collections.Generic;
using System;

[Serializable]
public class Dictionary<T>
{
    public List<string> Key = new();
    public List<T> Value = new();

    public bool ContainsKey(string key)
    {
        foreach (string item in Key)
            if (item == key)
                return true;

        return false;
    }

    public void Add(string key, T value)
    {
        Key.Add(key);
        Value.Add(value);
    }

    public T this[string key]
    {
        get
        {
            for (int i = 0; i < Key.Count; i++)
                if (Key[i] == key)
                    return Value[i];

            throw new IndexOutOfRangeException();
        }
        set
        {
            for (int i = 0; i < Key.Count; i++)
                if (Key[i] == key)
                    Value[i] = value;
        }
    }
}

[Serializable]
public class CloudPlayerPrefs
{
    public Dictionary<string> _stringPrefs = new();
    public Dictionary<int> _intPrefs = new();
    public Dictionary<float> _floatPrefs = new();

    public bool HasKey(string key)
    {
        if (_stringPrefs.ContainsKey(key))
            return true;

        if (_intPrefs.ContainsKey(key))
            return true;

        if (_floatPrefs.ContainsKey(key))
            return true;

        return false;
    }

    public void SetString(string key, string json)
    {
        if (_stringPrefs.ContainsKey(key))
            _stringPrefs[key] = json;
        else
            _stringPrefs.Add(key, json);
    }

    public void SetInt(string key, int saveObject)
    {
        if (_intPrefs.ContainsKey(key))
            _intPrefs[key] = saveObject;
        else
            _intPrefs.Add(key, saveObject);
    }

    public void SetFloat(string key, float saveObject)
    {
        if (_floatPrefs.ContainsKey(key))
            _floatPrefs[key] = saveObject;
        else
            _floatPrefs.Add(key, saveObject);
    }

    public string GetString(string key)
    {
        return _stringPrefs[key];
    }

    public int GetInt(string key)
    {
        return _intPrefs[key];
    }

    public float GetFloat(string key)
    {
        return _floatPrefs[key];
    }
}