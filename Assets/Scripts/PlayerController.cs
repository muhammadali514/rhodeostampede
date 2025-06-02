using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpHeight;
    public float jumpspeed;
    public Animator animator;
    bool hold;
    public GameObject animalObj;
    public Rigidbody rb;
    public float moveSpeed = 5f;
    bool movetonewObject;
    bool OneTimeadjust;
    public float tarnistionSpeed = 25f;
    public GameObject Sphere;

    bool movingTonewObject;
    void Start()
    {
        hold = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hold = true;
            movingTonewObject = animalObj;
            Sphere.SetActive(false);

            if(transform.parent == null && movingTonewObject)
            {
                Time.timeScale = 1f;
                StartCoroutine(Shift());
            }
        }
        
        if(Input.GetMouseButton(0))
        {
            hold = true;

            SittingonAnimal();
        }

        if(Input.GetMouseButtonUp(0))
        {
            //rb.isKinematic = false;
            animator.Play("Jump");
            //Sphere.SetActive(true);
            transform.parent.gameObject.GetComponent<AnimalController>().Control_able = false;
            transform.parent = null;
            StartJump();
            hold = false;

            rb.AddForce(Vector3.forward * moveSpeed * Time.deltaTime, ForceMode.Impulse);
        }

        if(!hold)
        {
            transform.Translate(Vector3.forward *moveSpeed * Time.deltaTime);
        }

        if(movetonewObject && hold)
        {
            // if(Vector3.Distance(transform.position, animalObj.transform.GetChild(2).transform.position) > 0.03f)
            // {
            //     Debug.Log("Yeh check kro");
            //     transform.position = Vector3.MoveTowards(transform.position,animalObj.transform.GetChild(2).transform.position,tarnistionSpeed * Time.deltaTime);
            // }
            // else
            // {
            //     Setted();
            // }
        }
    }

    void Setted()
    {
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;
        animator.Play("idle");
        // Get the position and rotation of the child object of the animal that represents the seating position
        Transform seatingPosition = animalObj.transform.GetChild(2);
        // Set the player's position and rotation to match the seating position of the new animal
        transform.position = seatingPosition.position;
        transform.rotation = seatingPosition.rotation;
        // Set the player as a child of the new animal
        transform.parent = animalObj.transform;
        // animator.Play("idle");
        // transform.parent = animalObj.transform;
        //movetonewObject = false;
    }

    void StartJump()
    {
        StartCoroutine(Jump());
    }

    IEnumerator Jump()
    {
        Vector3 targetPosition = transform.position + Vector3.up * jumpHeight;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        Time.timeScale = 0.65f;

        transform.position = targetPosition;
        

    }

    IEnumerator Shift()
    {
        Debug.Log("Shiftt");
        Vector3 targetPosition = animalObj.transform.GetChild(2).position;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, jumpspeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        transform.position = targetPosition;
        transform.parent = animalObj.transform;

    }

    bool SittingonAnimal()
    {
        bool sitting = false;

        if(this.transform.parent != null)
        {
            transform.parent.gameObject.GetComponent<AnimalController>().Control_able = true;
            sitting = true;
        }
        return sitting;
    }

}


