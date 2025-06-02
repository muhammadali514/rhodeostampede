using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public static GameController ins;

    public GameObject smash;
    public bool _died;
    public GameObject Player;
    public GameObject LooseScreen;
    bool GameStart;
    public GameObject Touchtoplaytxt;
    // Start is called before the first frame update
    void Start()
    {
        if(ins == null)
            ins = this;
        else
            Destroy(this.gameObject);

        _died = false;

        GameStart = false;
        Time.timeScale = 0f;
    }

    public void Died()
    {
        Time.timeScale = 1f;
        _died = true;
        Player.transform.parent = null;
        Player.GetComponent<Animator>().Play("Fall");
        Player.GetComponent<PlayerScript>().enabled = false;
        
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.transform.parent = null;
        Player.GetComponent<Rigidbody>().useGravity = true;
        smash.SetActive(true);
        StopAllCoroutines();

        Invoke("LooseScreenActivate",1.5f);

    }

    void LooseScreenActivate () 
    {
        Time.timeScale = 1f;
        LooseScreen.SetActive(true);
    }

    public void PlayAgain()
    {
        
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0) && !GameStart)
        {
            GameStart = true;
            Time.timeScale = 1f;
            Touchtoplaytxt.SetActive(false);
        }
    }
}
