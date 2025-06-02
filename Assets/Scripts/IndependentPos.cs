using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndependentPos : MonoBehaviour
{
    public Transform Player;
    private float initialHeight;
    private float distanceToGround = 5f;
    private Collider childCollider;
    float initx;
    float initz;
    private void OnEnable() 
    {
        childCollider = GetComponent<Collider>();
        if (Player != null)
        {
            // Store the initial height of the child object
            initialHeight = transform.position.y;
        }
        else
        {
            Debug.LogError("Player transform not assigned!");
        }

        initx = transform.position.x;
        initz = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null && childCollider != null)
        {
            // Cast a ray down from the player to detect the ground
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
            {
                // Calculate the new Y position to keep the child object on the ground
                float newY = hit.point.y + childCollider.bounds.extents.y;

                // Set the child object's position to stay on the ground
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }
        }
    }
}
