using Agava.YandexGames;
using UnityEngine;

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
            StartCoroutine(OnInitialized());
        }
    }

    private IEnumerator OnInitialized()
    {
        int threeSeconds = 3;
        int firsScene = 1;
        var waitForSeconds = new WaitForSeconds(threeSeconds);
        yield return waitForSeconds;
        SceneManager.LoadScene(firsScene);
    }
#endif
}
