using UnityEngine;

public static class GameObjectExtansions
{
    public static bool IsInLayer(this GameObject gameObject, LayerMask layer) =>
    layer == (layer | 1 << gameObject.layer);

    public static bool IsInLayer(this GameObject gameObject, LayerMask[] layers)
    {
        bool result = true;
        foreach (var layer in layers)
        {
            result &= layer == (layer | 1 << gameObject.layer);
        }
        return result;
    }
}
