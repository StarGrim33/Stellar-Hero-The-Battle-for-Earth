using UnityEngine;

namespace Player
{
    public interface IControllable
    {
        void Move();

        void TryDash();

        void SetDirection(Vector2 direction);
    }
}