using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _nextState;

    protected IDamageable Target { get; private set; }

    public State NextState => _nextState;

    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    public void Init(IDamageable target)
    {
        if (Target == null)
            Target = target;
    }
}
