using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SDKInitializer : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

//#if UNITY_WEBGL && !UNITY_EDITOR

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialized);

        if (YandexGamesSdk.IsInitialized)
            InterstitialAd.Show();
    }

    private void OnInitialized()
    {
        SceneManager.LoadScene(1);
    }
    //#endif
}
