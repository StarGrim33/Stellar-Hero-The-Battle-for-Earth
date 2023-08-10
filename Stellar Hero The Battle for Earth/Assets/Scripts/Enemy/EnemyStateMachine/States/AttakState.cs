using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttakState : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;

    private Animator _animator;
    private float _lastAttackTime;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_lastAttackTime <= 0)
        {

        }
    }

    private void Attack(IDamageable target)
    {
        _animator.Play(Constants.AttackState);
        target.TakeDamage(_damage);
    }
}
