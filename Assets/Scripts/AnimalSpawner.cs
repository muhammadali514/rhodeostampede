using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    public float minValue;
    public float maxValue;
    public float numberOfAnimalsToSpawn;
    public float spawnInterval;
    public float spawnRadius = 10f;
    public GameObject[] animalPrefabs;
    public Vector3 _offset;
    public Transform Playertran;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnAnimalsRoutine());
    }

    IEnumerator SpawnAnimalsRoutine()
    {
        while (!GameController.ins._died)
        {
            SpawnAnimals();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void Update() 
    {   
        Vector3 transpath = new Vector3 (0,0,Playertran.position.z);
        transform.position = transpath + _offset;    
    }

    void SpawnAnimals()
    {
        for (int i = 0; i < numberOfAnimalsToSpawn; i++)
        {
            Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
            randomPosition.y = 0f;

            int Randomanimal = Random.Range(0,animalPrefabs.Length);

            
            Instantiate(animalPrefabs[Randomanimal], transform.position + randomPosition, Quaternion.identity);
        }
    }

    
}
