using UnityEngine;

public class DroneSpriteRotator : MonoBehaviour
{
    private SpriteRenderer _droneSprite;
    private DroneAttackState _droneAttackState;

    private void Start()
    {
        _droneSprite = GetComponent<SpriteRenderer>();
    }

    public void Init(DroneAttackState attackState)
    {
        _droneAttackState = attackState;
    }

    private void Update()
    {
        RotateToTarget();
    }

    private void RotateToTarget()
    {
        if (_droneSprite != null && _droneAttackState.CurrenTarget != null)
        {
            Transform target = _droneAttackState.CurrenTarget;

            if (target.position.x > transform.position.x)
            {
                _droneSprite.flipX = false; 
            }
            else
            {
                _droneSprite.flipX = true;
            }
        }
    }
}
