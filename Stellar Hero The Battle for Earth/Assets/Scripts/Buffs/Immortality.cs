using System.Collections;
using UnityEngine;

public class Immortality : Buff
{
    public override void Take(PlayerHealth playerHealth)
    {
        playerHealth.ActivateImmortal();
        Sprite.enabled = false;
        StartCoroutine(DisableEffect(playerHealth));
    }

    private IEnumerator DisableEffect(PlayerHealth playerHealth)
    {
        var waitForSeconds = new WaitForSeconds(Duration);
        yield return waitForSeconds;
        Destroy(gameObject);
    }
}
