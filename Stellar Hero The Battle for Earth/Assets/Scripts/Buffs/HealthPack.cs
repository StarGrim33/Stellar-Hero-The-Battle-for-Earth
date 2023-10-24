using UnityEngine;

public class HealthPack : Buff
{
    [SerializeField] private int _healing;
    
    public override void Take(PlayerHealth playerHealth)
    {
        playerHealth.HealUp(_healing);
        Destroy(gameObject);
    }
}

