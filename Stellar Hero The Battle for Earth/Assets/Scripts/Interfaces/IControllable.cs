using UnityEngine;

public interface IControllable
{
    void Move();

    void TryDash();

    void SetDirection(Vector2 direction);
}
