using UnityEngine;

public class PlayerShooting : MonoBehaviour, IWeapon
{
     
    public void PerformAttack()
    {
        Debug.Log("Pew pew");
    }
}
