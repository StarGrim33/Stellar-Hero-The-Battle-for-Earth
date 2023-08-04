using UnityEngine;

public class AimBot : MonoBehaviour
{
    private float _distance = 500f;
    private Transform _target;
    [SerializeField] private Transform _startPosition;

    private void Update()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, _distance);
        Debug.DrawRay(_startPosition.position, -transform.up * _distance, Color.red);

        if(hit.transform != null && hit.transform.gameObject.TryGetComponent<EnemyUnit>(out EnemyUnit unit))
        {
            Debug.Log(unit.Config.name);
        }
    }
}
