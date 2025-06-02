using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{
    public bool preexist;
    public GameObject[] hurdlePrefab;
    
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !GameController.ins._died)
        {
            PathSapwner.ins.SpawnNext();
            
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!GameController.ins._died)
            Destroy(this.gameObject,5f);
        }
    }

    private void OnEnable() 
    {
        if(!preexist)
        CreateHurdles();
            
    }

    void CreateHurdles()
    {
        int maxHurdles = Random.Range(1,4);

        for (int i = 0; i < maxHurdles; i++)
        {
            int randomhurdle = Random.Range(0,hurdlePrefab.Length);
            float randomX = Random.Range(-5f, 5f);
            float randomZ = Random.Range(-5f, 5f);

            Vector3 randomLocalPosition = new Vector3(randomX, 3f, randomZ);

            GameObject hurdle = Instantiate(hurdlePrefab[randomhurdle],transform);
            hurdle.transform.localPosition = randomLocalPosition;
        }
    }
}
