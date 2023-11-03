using Agava.WebUtility;
using UnityEngine;

public class WebUtilityFixer : MonoBehaviour
{
    [SerializeField] private GameSettings _environment;

    private void Awake()
    {
        if(Device.IsMobile)
        {
            _environment.IsMobile = true;
            Debug.Log("Mobile is initialized");
        }
    }

    private void OnEnable()
    {
        WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;
    }

    private void OnDisable()
    {
        WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
    }

    private void OnInBackgroundChange(bool inBackground)
    {
        AudioListener.pause = inBackground;
        AudioListener.volume = inBackground ? 0f : 1f;
    }
}
