using UnityEngine;
using UnityEngine.UI;

public class SphereCuboidOverlap : MonoBehaviour
{
    public GameObject sphere;
    public GameObject cuboid;

    public GameObject Overlap_txt;

    void Update()
    {
        // Check for overlap
        bool overlap = DoesOverlap(sphere, cuboid);
        Overlap_txt.SetActive(overlap);
        Debug.Log("Overlap: " + overlap);
    }

    bool DoesOverlap(GameObject sphere, GameObject cuboid)
    {
        // Get sphere properties
        Vector3 sphereCenter = sphere.transform.position;
        float sphereRadius = sphere.transform.localScale.x / 2f; // Assuming uniform scaling

        // Get cuboid properties
        Vector3 cuboidCenter = cuboid.transform.position;
        Vector3 cuboidSize = cuboid.transform.localScale;

        // Calculate cuboid extents
        Vector3 cuboidExtents = cuboidSize / 2f;

        // Calculate distance between sphere center and cuboid center
        Vector3 distance = sphereCenter - cuboidCenter;

        // Clamp distance to the cuboid extents
        Vector3 clamped = new Vector3(
            Mathf.Clamp(distance.x, -cuboidExtents.x, cuboidExtents.x),
            Mathf.Clamp(distance.y, -cuboidExtents.y, cuboidExtents.y),
            Mathf.Clamp(distance.z, -cuboidExtents.z, cuboidExtents.z)
        );

        // Calculate closest point on cuboid to the sphere center
        Vector3 closestPoint = cuboidCenter + clamped;

        // Calculate distance between sphere center and closest point on cuboid
        float distanceSquared = (sphereCenter - closestPoint).sqrMagnitude;

        // Check for overlap
        return distanceSquared <= sphereRadius * sphereRadius;
    }

    // non uniform scaling

    // Vector3 sphereCenter = sphere.transform.position;
    //     Vector3 sphereScale = sphere.transform.localScale;
    //     float sphereRadius = Mathf.Max(sphereScale.x, sphereScale.y, sphereScale.z) / 2f; // Taking maximum scale component as radius

    //     // Get cuboid properties
    //     Vector3 cuboidCenter = cuboid.transform.position;
    //     Vector3 cuboidSize = cuboid.transform.localScale;

    //     // Calculate cuboid extents
    //     Vector3 cuboidExtents = cuboidSize / 2f;

    //     // Calculate distance between sphere center and cuboid center
    //     Vector3 distance = sphereCenter - cuboidCenter;

    //     // Clamp distance to the cuboid extents
    //     Vector3 clamped = new Vector3(
    //         Mathf.Clamp(distance.x, -cuboidExtents.x, cuboidExtents.x),
    //         Mathf.Clamp(distance.y, -cuboidExtents.y, cuboidExtents.y),
    //         Mathf.Clamp(distance.z, -cuboidExtents.z, cuboidExtents.z)
    //     );

    //     // Calculate closest point on cuboid to the sphere center
    //     Vector3 closestPoint = cuboidCenter + clamped;

    //     // Calculate distance between sphere center and closest point on cuboid
    //     float distanceSquared = (sphereCenter - closestPoint).sqrMagnitude;

    //     // Check for overlap
    //     return distanceSquared <= sphereRadius * sphereRadius;
}
