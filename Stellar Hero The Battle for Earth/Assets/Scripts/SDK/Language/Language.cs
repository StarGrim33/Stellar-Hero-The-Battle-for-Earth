using Agava.YandexGames;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class Language : MonoBehaviour
{
    [DllImport("__Internal")] private static extern string GetYandexGamesSdkEnvironment();
    [SerializeField] private string _currentLanguage = "en";

    public string CurrentLanguage => _currentLanguage;

    public static Language Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);

            _currentLanguage = YandexGamesSdk.Environment.i18n.lang;

            Console.WriteLine("!!!Language - " + _currentLanguage);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
