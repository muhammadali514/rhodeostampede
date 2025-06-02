using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    public float rot_speed;
    
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,rot_speed * Time.deltaTime,0);
    }
}
