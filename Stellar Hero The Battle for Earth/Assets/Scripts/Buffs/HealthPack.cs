using UnityEngine;

public class HealthPack : BaseBuff
{
    [SerializeField] private int _healValue;
    
    public override void Take(PlayerHealth playerHealth)
    {
        playerHealth.HealUp(_healValue);
        Destroy(gameObject);
    }
}

