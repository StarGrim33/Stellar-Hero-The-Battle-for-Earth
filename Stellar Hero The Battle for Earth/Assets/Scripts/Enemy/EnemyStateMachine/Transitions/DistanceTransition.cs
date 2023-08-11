using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionRangeForNextState;
    [SerializeField] private float _spreadRange;

    private void Start()
    {
        _transitionRangeForNextState += Random.Range(-_spreadRange, _spreadRange);
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, Target.position) < _transitionRangeForNextState)
            NeedTransit = true;
    }
}
