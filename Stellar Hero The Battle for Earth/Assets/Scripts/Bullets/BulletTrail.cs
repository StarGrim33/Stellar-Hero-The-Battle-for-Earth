using UnityEngine;

public class BulletTrail : MonoBehaviour
{
    [SerializeField] private float _speed = 40f;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private float _progress;
    private float _axisValue = -1;

    private void Start()
    {
        _startPosition = transform.position.WithAxis(Axises.z, value: _axisValue);
    }

    private void Update()
    {
        _progress += Time.deltaTime;
        transform.position = Vector3.Lerp(_startPosition, _targetPosition, _progress);
    }

    public void SetTargetPosition(Vector3 targetPosition)
    {
        _targetPosition = targetPosition.WithAxis(Axises.z, value: _axisValue);
    }
}
