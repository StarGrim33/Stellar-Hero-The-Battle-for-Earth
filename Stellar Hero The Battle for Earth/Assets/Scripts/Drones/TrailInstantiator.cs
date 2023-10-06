using UnityEngine;

public class TrailInstantiator : MonoBehaviour
{
    [SerializeField] private GameObject _trailPrefab;
    [SerializeField] private Transform _gunPoint;

    public void TrailInstantiate(Vector3 targetPosition)
    {
        var trail = Instantiate(_trailPrefab, _gunPoint.transform.position, Quaternion.identity);
        var trailScript = trail.GetComponent<BulletTrail>();

        trailScript.SetTargetPosition(targetPosition);
    }
}
