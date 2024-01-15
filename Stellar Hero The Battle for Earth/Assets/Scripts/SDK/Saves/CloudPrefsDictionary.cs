using System;
using System.Collections.Generic;

namespace SDK
{
    public class CloudPrefsDictionary<T>
    {
        public List<string> Key = new();
        public List<T> Value = new();

        public bool ContainsKey(string key)
        {
            foreach (string item in Key)
            {
                if (item == key)
                {
                    return true;
                }
            }

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
                {
                    if (Key[i] == key)
                    {
                        return Value[i];
                    }
                }

                throw new IndexOutOfRangeException();
            }
            set
            {
                for (int i = 0; i < Key.Count; i++)
                {
                    if (Key[i] == key)
                    {
                        Value[i] = value;
                    }
                }
            }
        }
    }
}