using System.Collections;
using TMPro;
using UnityEngine;

public class Animals : MonoBehaviour
{
    public float speed;
    public float RotationSpeed;
    public SkinnedMeshRenderer skinedmesh;
    public bool _controlable;
    public Material highlightMat;
    public Material normalMAt;
    public Material angeryMat;
    bool singleRide = false;
    public bool Reduce;
    private Vector3 dragOrigin;
    public Animator animator;
    public AudioSource animalsound;

    private Color startColor;
    private Color targetColor = Color.red;

    //public TextMeshPro text;
    public float duration = 10f;

    bool isDragging;
    float time;
    
    
    public bool Controlable
    {
        get { return _controlable; }
        set { _controlable = value; }
    }

    private void Start() 
    {
        Physics.IgnoreLayerCollision(7,8);

        if(!Controlable)
        speed = Random.Range(3,8);     

        time = 0;


        startColor = skinedmesh.material.color;
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging && Controlable)
        {
            Vector3 difference = Input.mousePosition - dragOrigin;

            float rotation = difference.x * RotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotation, Space.World);

            dragOrigin = Input.mousePosition;
        }

        if(Controlable)
        {
            // text.gameObject.SetActive(true);

             time += 1 * Time.deltaTime;
            // text.text = time.ToString();

            if(time >= 10f)
            {
                skinedmesh.material = angeryMat;
                float t = Mathf.PingPong(Time.time / duration, 1f); 
                skinedmesh.material.color = Color.Lerp(startColor, targetColor, t);

                if(time >= 15f)
                {
                    skinedmesh.material.color = targetColor;
                    ThrowPlayer();
                }

            }
        }
        else
        {
            //text.gameObject.SetActive(false);
        }
        

        if(Reduce)
        {
            if(!Controlable && !singleRide)
            {
                singleRide = true;
                ReduceSpeedCall();
            }
        }        
    }

    public void Highlight(bool highlight)
    {
        if(highlight)
            skinedmesh.material = highlightMat;
        else
            skinedmesh.material = normalMAt;
    }

    public void ReduceSpeedCall()
    {
        speed = 5f;
    }

    public void ThrowPlayer()
    {
        if(Controlable)
        {
            speed = 0f;
            Controlable = false;
            animator.Play("anger");

            transform.Find("Player").gameObject.GetComponent<PlayerScript>().PlayerThrown();
        }
        
    }
    

    IEnumerator ReduceSpeed()
    {
        while (speed > 1f)
        {
            speed -= 0.5f;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(this.gameObject);
    }

    public void RidingOn (bool ride) 
    {
       if(ride)
        {
            // Debug.Log("Riding On "+speed);
            Controlable = true;
            speed = 15f;
        }
       else
        {
            Controlable = false;
            speed = 9f;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Animal"))
        {
            if(other.gameObject.GetComponent<Animals>().Controlable || this.Controlable)
            {
                GameController.ins.Died();
                Destroy(this.gameObject,1f);
            }
        }    
    }
}
