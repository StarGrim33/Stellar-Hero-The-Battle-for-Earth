using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected IDamageable Target { get; set; }

    public void Enter(IDamageable target)
    {
        if(!enabled)
        {
            Target = target;
            enabled = true;

            foreach(Transition transition in _transitions) 
            {
                transition.enabled = true;
                transition.Init(Target);
            }
        }
    }

    public State GetNextState()
    {
        foreach(Transition transition in _transitions)
        {
            if(transition.NeedTransit)
                return transition.NextState;
        }

        return null;
    }

    public void Exit()
    {
        if(enabled)
        {
            foreach (Transition transition in _transitions)
                transition.enabled = false;

            enabled = false;
        }
    }
}
