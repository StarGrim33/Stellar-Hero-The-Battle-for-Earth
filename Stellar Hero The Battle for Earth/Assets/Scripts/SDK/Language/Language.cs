using System.Runtime.InteropServices;
using Agava.YandexGames;
using UnityEngine;

namespace SDK
{
    public class Language : MonoBehaviour
    {
        [DllImport("__Internal")] private static extern string GetYandexGamesSdkEnvironment();

        public static Language Instance;

        [SerializeField] private string _currentLanguage = "en";

        public string CurrentLanguage => _currentLanguage;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
                _currentLanguage = YandexGamesSdk.Environment.i18n.lang;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}