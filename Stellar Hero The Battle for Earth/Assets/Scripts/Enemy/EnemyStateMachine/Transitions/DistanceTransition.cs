using UnityEngine;

namespace Enemy
{
    public class DistanceTransition : Transition
    {
        [SerializeField] private float _transitionRangeForNextState;

        private void Update()
        {
            if (Target.IsAlive && Vector2.Distance(transform.position, Target.TargetTransform.position) < _transitionRangeForNextState)
                NeedTransit = true;
        }
    }
}
