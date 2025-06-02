using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PathSapwner : MonoBehaviour
{
    public static PathSapwner ins;
    public GameObject Path;
    public Transform environment;

    private void Start() 
    {
        if(ins == null)
            ins = this;
        else
            Destroy(this);
    }
    
    public void SpawnNext() 
    {   
        float x = environment.GetChild(environment.childCount -1 ).transform.position.x;
        float y = environment.GetChild(environment.childCount -1 ).transform.position.y;
        float valz = environment.GetChild(environment.childCount -1 ).transform.position.z + 50f;
        Vector3 newPos = new Vector3(x,y,valz);
        
        GameObject _path = Instantiate(Path);
        _path.transform.SetParent(environment);
        _path.transform.position = newPos;
    }
}
