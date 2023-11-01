using System.Collections;
using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    public float Timer { get; set; }

    public abstract void Take(PlayerHealth playerHealth);
}
