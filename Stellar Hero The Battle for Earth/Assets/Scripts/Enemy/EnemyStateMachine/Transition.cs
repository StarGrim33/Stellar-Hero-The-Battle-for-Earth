using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _nextState;

    protected Transform Target { get; private set; }

    public State NextState => _nextState;

    public bool NeedTransit { get; protected set; }

    private void OnEnable()
    {
        NeedTransit = false;
    }

    public void Init(Transform target)
    {
        if (Target == null)
            Target = target;
    }
}
