using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Bootstrap _bootstrap;

    public bool IsMobile { get; set; } = true;

    private void Start()
    {
        Debug.Log($"Ismobile - {IsMobile}");
    }
}
