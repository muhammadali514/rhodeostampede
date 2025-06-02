using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustParticles : MonoBehaviour
{
    public GameObject ps;
    private Camera mainCamera;

    private void Start() 
    {
        mainCamera = Camera.main;
    }

    private void Update() {
        // // Check if the object is within the camera's view
        // if (IsInView())
        // {
        //     Debug.Log("Object is in camera view");
        // }
        // else
        // {
        //     Debug.Log("Object is not in camera view");
        // }
    }

    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("ground"))    
        {
            GameObject pdust = Instantiate(ps,transform.position,Quaternion.Euler(180,0,0));
            Destroy(pdust,0.5f);
        }
    }

    bool IsInView()
    {
        // Get the object's position in viewport space
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(transform.position);

        // Check if the object's position is within the viewport bounds
        bool inView = viewportPoint.x >= 0 && viewportPoint.x <= 1 && viewportPoint.y >= 0 && viewportPoint.y <= 1 && viewportPoint.z > 0;

        return inView;
    }
}
