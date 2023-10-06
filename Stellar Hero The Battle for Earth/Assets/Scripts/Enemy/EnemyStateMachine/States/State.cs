using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;
    private EnemyHealth _health;

    protected IDamageable Target { get; set; }

    protected EnemyHealth Health => _health;

    private void Awake()
    {
        _health = GetComponent<EnemyHealth>();
    }

    public void Enter(IDamageable target)
    {
        if(enabled == false)
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
