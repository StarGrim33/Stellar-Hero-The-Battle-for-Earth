namespace SDK
{
    public class CloudPlayerPrefs
    {
        public CloudPrefsDictionary<string> _stringPrefs = new();
        public CloudPrefsDictionary<int> _intPrefs = new();
        public CloudPrefsDictionary<float> _floatPrefs = new();

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

        public void SetInt(string key, int saveObject)
        {
            if (_intPrefs.ContainsKey(key))
                _intPrefs[key] = saveObject;
            else
                _intPrefs.Add(key, saveObject);
        }

        public int GetInt(string key)
        {
            return _intPrefs[key];
        }
    }
}