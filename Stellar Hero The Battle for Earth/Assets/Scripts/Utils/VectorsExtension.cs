using UnityEngine;

public static class VectorsExtension
{
    public static Vector3 WithAxis(this Vector3 vector, Axises axis, float value)
    {
        return new Vector3
            (axis == Axises.x ? value : vector.x, axis == Axises.y ? value : vector.y, axis == Axises.z ? value : vector.z);
    }
}
