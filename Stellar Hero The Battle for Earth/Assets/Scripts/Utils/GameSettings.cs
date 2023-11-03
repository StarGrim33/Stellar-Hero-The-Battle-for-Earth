using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [SerializeField] private Bootstrap _bootstrap;

    public bool IsMobile { get; set; }
}
