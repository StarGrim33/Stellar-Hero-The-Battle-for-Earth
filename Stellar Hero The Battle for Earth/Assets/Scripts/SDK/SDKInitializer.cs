using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SDKInitializer : MonoBehaviour
{
    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

#if UNITY_WEBGL && !UNITY_EDITOR

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize();

        if (YandexGamesSdk.IsInitialized)
        {
            InterstitialAd.Show();

            Debug.Log("SDK is initialized");

            StartCoroutine(OnInitialized());
        }
    }

    private IEnumerator OnInitialized()
    {
        var waitForSeconds = new WaitForSeconds(3f);
        yield return waitForSeconds;
        SceneManager.LoadScene(1);
    }
#endif
}
