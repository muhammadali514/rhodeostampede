using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour // This is an object triggers up when collides with animal
{
    public GameObject Highlighted_Animal;
    public Transform Player;
    
    private void OnEnable() 
    {
        Highlighted_Animal = null;    
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Animal") && Highlighted_Animal == null)
        {
            Highlighted_Animal = other.gameObject;
            Highlighted_Animal.GetComponent<Animals>().Highlight(true);
            Player.GetComponent<PlayerScript>().AnimaltoShift = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.CompareTag("Animal") && Highlighted_Animal == null)
        {
            Highlighted_Animal = other.gameObject;
            Highlighted_Animal.GetComponent<Animals>().Highlight(true);
            Player.GetComponent<PlayerScript>().AnimaltoShift = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Animal"))
        {
            Highlighted_Animal = null;
            other.gameObject.GetComponent<Animals>().Highlight(false);
            Player.GetComponent<PlayerScript>().AnimaltoShift = null;
        }
    }

    private void OnDisable() 
    {
        if(Highlighted_Animal)
        {
            Highlighted_Animal.gameObject.GetComponent<Animals>().Highlight(false);
        }
    }
}
