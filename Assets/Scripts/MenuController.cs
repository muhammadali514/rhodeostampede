using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public ParticleSystem ps;
    
    public void StartGame()
    {
        StartCoroutine(Start_Game());
    }

    IEnumerator Start_Game()
    {
        ps.Play();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(1);
    }
}
