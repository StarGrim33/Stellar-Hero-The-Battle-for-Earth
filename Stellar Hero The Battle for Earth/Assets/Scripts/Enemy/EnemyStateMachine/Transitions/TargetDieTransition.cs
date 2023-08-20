using UnityEngine;

public class TargetDieTransition : Transition
{
    private void Update()
    {
        if (Target.IsAlive == false)
            NeedTransit = true;
    }
}
