using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Values")]
    public float jumpHeight = 2f;
    public float jumpSpeed = 2f;
    public float transitionSpeed;
    public float forwardspeeed = 5f;
    public float slowmo = 0.05f;
    public ParticleSystem ps;
    bool jump;
    //public GameObject HighlightCircle;
    bool _shift;
    public AudioSource yehhay;
    public AudioSource jumpsound;

    [Space]
    [Space]
    public Rigidbody rb;
    public Animator anim;
    public GameObject Highlighter;
    private bool _ridding;
    public bool Ridding
    {
        get { return _ridding; }
        set { _ridding = value; }
    }

    //[HideInInspector]
    public GameObject AnimaltoShift;
    // Start is called before the first frame update
    void Start()
    {
        ControlableAnimal(CheckForRidding());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !GameController.ins._died)
        {
            jump = false;
            Ridding = CheckForRidding();
            
            
            if(!Ridding)
            {
                // Time.timeScale=1f;
                Highlighter.SetActive(false);
                //Shift
                if(AnimaltoShift != null)
                {
                    yehhay.Play();
                    rb.useGravity = false;
                    _shift = true;
                    anim.Play("JumpingDown");
                    // ShiftonAnimal();
                }
                else
                {
                    anim.Play("Fall");
                    Time.timeScale = 1f;
                    _shift = false;
                }
            }
            else
            {
                transform.parent.GetComponent<Animals>().animalsound.Play();
            }
        }

        // if(Input.GetMouseButton(0))
        // {
            
        // }

        if(Input.GetMouseButtonUp(0))
        {
            jump = true;
            AnimaltoShift = null;

            if(transform.parent != null && !GameController.ins._died)
            {
                transform.parent.GetComponent<Animals>().Controlable = false;
                transform.parent.GetComponent<Animals>().Reduce = true;
                transform.parent.GetComponent<AudioSource>().enabled = false;
                Quaternion angle = transform.parent.rotation;

                transform.parent = null;
                transform.rotation = angle;
                rb.useGravity = true;
            

                Highlighter.SetActive(true);
                jumpsound.Play();
                anim.Play("Jump");
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                rb.AddForce(transform.forward * forwardspeeed, ForceMode.Impulse);
                //HighlightCircle.SetActive(true);
                

                StartCoroutine(StartJump());
            }
        }

        // if(jump) //// With raycast
        // {
        //     //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward + Vector3.down) * 1000, Color.white);
        //     RaycastHit hit;

        //     if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity,layer))
        //     {
        //         //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward + Vector3.down) * hit.distance, Color.yellow);
                
        //             HighlightCircle.transform.position = Vector3.Lerp(HighlightCircle.transform.position, hit.point, Time.deltaTime * smoothness);
        //             //HighlightCircle.transform.position = hit.point;                
        //     }
        // }
        // else
        // {
        //     HighlightCircle.SetActive(false);
        // }

        

        if(_shift && AnimaltoShift != null)
        {
            if(Vector3.Distance(transform.position, AnimaltoShift.transform.position) > 0.30f)
            {
                // Debug.Log("Alphaaaa shift Update "+Vector3.Distance(transform.position, AnimaltoShift.transform.position));
                transform.position = Vector3.MoveTowards(transform.position, AnimaltoShift.transform.position, transitionSpeed * Time.deltaTime);
                
            }
            else
            {
                _shift = false;
                ShiftonAnimal();
            }
        }
    }

    IEnumerator CheckForGravity()
    {
        float time = 0;
        while(jump)
        {
            time += 0.01f;
            //Debug.Log("Value  "+time);
            if(time > 7f)
            {
                Time.timeScale = 1f;
                StopAllCoroutines();
            }

            yield return null;
        }
    }


    IEnumerator StartJump()
    {
        StartCoroutine(CheckForGravity());
        
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = slowmo;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        // while(!CheckForRidding())
        // {
        //     RaycastHit hit;
        // if (Physics.SphereCast(transform.position, 5f,transform.forward + Vector3.down * 0.5f, out hit, 5f))
        // {
        //     Debug.Log("Alphaa  Ins circle  ");
        //     // Check if the hit object is an animal
        //     if (hit.collider.CompareTag("Animal"))
        //     {
        //         Debug.Log("Bravoo  Hitt on Animal");

        //         // Highlight the animal by changing its material or applying a glow effect
        //         Animals animal = hit.collider.GetComponent<Animals>();
        //         if (animal != null)
        //         {
        //             AnimaltoShift = animal.gameObject;
        //             animal.Highlight(true);
        //         }
        //         else
        //         {
        //             AnimaltoShift = null;
        //             animal.Highlight(false);
        //         }
        //     }

        //     // Update the position of the circle object
        //     if (instantiatedCircle != null)
        //     {
        //         Debug.Log("Alphaa  Ins circle moving ");
        //         instantiatedCircle.transform.position = new Vector3(hit.point.x, 0.01f, hit.point.z);
        //     }
        //     else
        //     {
        //         // Instantiate a circle object on the ground if it's not already instantiated
        //         instantiatedCircle = Instantiate(circlePrefab, new Vector3(hit.point.x, 0.01f, hit.point.z), Quaternion.Euler(-90,0,0));
        //     }
        // }
        // else
        // {
        //     // Destroy the circle object if the raycast didn't hit anything
        //     Destroy(instantiatedCircle);
        // }
        // yield return null;
        // }
        

        

    }
    

    void ShiftonAnimal()
    {
        // anim.Play("JumpingDown");        

        // yield return new WaitForSeconds(0.5f);
        // while (Vector3.Distance(transform.position, AnimaltoShift.transform.position) > 0.05f)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, AnimaltoShift.transform.position, transitionSpeed);
        //     yield return null; // Wait for the next frame
        // }
        //StopAllCoroutines();

        Time.timeScale=1f;
        ps.Play();
        
        rb.velocity = Vector3.zero;
        transform.position = AnimaltoShift.transform.GetChild(2).transform.position;
        transform.parent = AnimaltoShift.transform;
        transform.rotation = Quaternion.Euler(0,0,0);
        anim.Play("idle");
        Highlighter.SetActive(false);
        ControlableAnimal(transform.parent);

        PlaySound();
        //StartCoroutine(Shift());
    }

    void PlaySound()
    {
        transform.parent.GetComponent<Animals>().animalsound.Play();
        
        // AudioClip clip;

        // clip = transform.parent.GetComponent<Animals>().animalsound;
        // transform.parent.GetComponent<AudioSource>().clip = clip;
        // transform.parent.GetComponent<AudioSource>().Play();

        // yield return new WaitForSeconds(clip.length);

        // clip = transform.parent.GetComponent<Animals>().animalrunning;
        // transform.parent.GetComponent<AudioSource>().clip = clip;
        // transform.parent.GetComponent<AudioSource>().Play();
        // transform.parent.GetComponent<AudioSource>().loop = true;
        
    }

    void ControlableAnimal(bool control)
    {
        transform.parent.GetComponent<Animals>().RidingOn(control);
        
    }
    bool CheckForRidding(){return transform.parent;}



    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("ground") && !_shift)    
        {
            GameController.ins.Died();
        }
        if(other.gameObject.CompareTag("walls"))
        {
            GameController.ins.Died();
        }
    }

    public void PlayerThrown()
    {
        if(transform.parent != null && !GameController.ins._died)
            {
                transform.parent.GetComponent<Animals>().Controlable = false;
                transform.parent.GetComponent<AudioSource>().enabled = false;
                Quaternion angle = transform.parent.rotation;

                transform.parent = null;
                transform.rotation = angle;
                rb.useGravity = true;
            

                Highlighter.SetActive(true);
                jumpsound.Play();
                anim.Play("Jump");
                rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
                rb.AddForce(transform.forward * forwardspeeed, ForceMode.Impulse);
                //HighlightCircle.SetActive(true);

                StartCoroutine(StartJump());
            }
        // Debug.Log("Plllllllll");  
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        Time.timeScale = 1f;
        rb.isKinematic = true;
        //HighlightCircle.SetActive(false);
    }
}