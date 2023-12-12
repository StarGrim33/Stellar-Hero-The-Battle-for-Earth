using System.Collections;
using UnityEngine;

public interface IBullet
{
    public void Shot(Vector3 startPoint, Vector3 endPoint, float speed, int damag);
}
