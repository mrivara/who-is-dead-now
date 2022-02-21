using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

    public float loadDelay;
    int scene;
    public string scene2;
    public Color loadToColor = Color.white;


    void Start()
    {

        if (loadDelay == 0)
        {        }
        else
        {        
            Invoke("LoadnLevel", loadDelay);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(1);
        }
    }

    public void LoadnLevel()
    {
        //Initiate.Fade(scene2, loadToColor, 0.5f);
        SceneManager.LoadScene(1);
    }

}