using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class TestPlayerScript : MonoBehaviour
{
    public float jumpHeight = 2f;
    public float jumpSpeed = 2f;
    public float Pushspeed;
    public Rigidbody rb;
    public Animator anim;
    public Transform onjecttoget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {

        }

        if(Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            rb.velocity = Vector3.zero;
            transform.position = onjecttoget.position;
            transform.parent = onjecttoget;
        }

        if(Input.GetMouseButtonUp(0))
        {
            rb.AddForce(Vector3.forward * Pushspeed, ForceMode.Impulse);
            StartJump();
        }
    }

    void StartJump()
    {
        //Highlighter.SetActive(true);
        anim.Play("Jump");
        StartCoroutine(Jump());
    }

    IEnumerator Jump()
    {
        Vector3 targetPosition = transform.position + Vector3.up * jumpHeight;

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, jumpSpeed * Time.deltaTime);
            yield return null;
        }
        

        Time.timeScale = 0.50f;

        transform.position = targetPosition;
    }
}
