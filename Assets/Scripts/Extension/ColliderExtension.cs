using UnityEngine;

public static class ColliderExtension
{
    public static Vector3 PickARandomPoint(this Collider collider)
    {
        Bounds bounds = collider.bounds;
        Vector3 v = bounds.center + new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-0.2f, 0.2f),
            Random.Range(-1f, 1f));
        return v;
    }
}
