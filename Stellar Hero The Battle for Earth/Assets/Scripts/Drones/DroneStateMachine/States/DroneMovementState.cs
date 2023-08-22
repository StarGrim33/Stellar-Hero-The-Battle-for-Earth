using UnityEngine;

public class DroneMovementState : IStateSwitcher
{
    private Transform _droneTransform;
    private Transform _heroTransform;
    private float _radius = 1.0f; 
    private float _angle = 0.0f; 

    public DroneMovementState(Transform transform, Transform playerTransform)
    {
        _droneTransform = transform;
        _heroTransform = playerTransform;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        _angle += Time.deltaTime;
        float x = _heroTransform.position.x + _radius * Mathf.Cos(_angle);
        float y = _heroTransform.position.y + _radius * Mathf.Sin(_angle);
        _droneTransform.position = new Vector3(x, y, _droneTransform.position.z);
    }
}
