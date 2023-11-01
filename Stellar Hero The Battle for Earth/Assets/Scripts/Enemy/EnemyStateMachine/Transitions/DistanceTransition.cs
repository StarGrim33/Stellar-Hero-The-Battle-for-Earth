using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionRangeForNextState;
    [SerializeField] private float _spreadRange;

    private void Update()
    {
        if (Target.IsAlive)
            if (Vector2.Distance(transform.position, Target.TargetTransform.position) < _transitionRangeForNextState)
                NeedTransit = true;
    }
}
