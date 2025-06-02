using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour // This controll animals for movement and speed
{
    public float speed;
    public Rigidbody rb;
    public bool Control_able;
    bool oneTimer;
    public float Rotationaspeed;
    public Material[] matts;

    
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        
        if (Input.GetMouseButton(0) && Control_able)
        {
            float rotationAmount = Input.GetAxis("Mouse X") * Rotationaspeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationAmount);
        }
        else
        {
            if(!oneTimer && !Control_able)
            {
                oneTimer = true;
                Invoke("ChangeDirection", 0.2f);
            }
        }
    }

    void ChangeDirection()
    {
        transform.rotation = Quaternion.identity;
    }

    public void HighlighMaterial(bool highlight)
    {
        if(highlight)
        {
            transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material = matts[1];
        }
        else
        {
            transform.GetChild(1).gameObject.GetComponent<SkinnedMeshRenderer>().material = matts[0];
        }
    }

}
