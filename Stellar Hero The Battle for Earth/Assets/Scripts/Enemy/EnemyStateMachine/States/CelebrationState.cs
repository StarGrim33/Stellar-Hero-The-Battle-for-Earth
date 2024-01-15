using UnityEngine;
using Utils;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class CelebrationState : State
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _animator.Play(Constants.CelebrationState);
        }

        private void OnDisable()
        {
            _animator.StopPlayback();
        }
    }
}
