using UnityEngine;

public interface IControllable
{
    public void Move();

    public void TryDash();

    public void SetDirection(Vector2 direction);
}
