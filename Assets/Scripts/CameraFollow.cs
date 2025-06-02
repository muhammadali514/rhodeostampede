using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothness = 0.125f;
    public Vector3 offSet;
    
    private void FixedUpdate() 
    {
        Vector3 _desiredpos = target.position + offSet;
        Vector3 _smoothedPos = Vector3.Lerp(transform.position, _desiredpos,smoothness);
        transform.position = _smoothedPos;

        transform.LookAt(target);
    }
}
