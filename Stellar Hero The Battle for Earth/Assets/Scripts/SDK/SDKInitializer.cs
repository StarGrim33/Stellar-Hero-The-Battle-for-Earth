using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SDK
{
    public class SDKInitializer : MonoBehaviour
    {
        private readonly float _threeSecondsDelay = 3f;
        private WaitForSeconds _delay;

        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
            _delay = new WaitForSeconds(_threeSecondsDelay);
        }

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
            int firsScene = 1;
            yield return _delay;
            SceneManager.LoadScene(firsScene);
        }
    }
}