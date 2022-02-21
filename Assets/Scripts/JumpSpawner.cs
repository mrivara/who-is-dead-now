using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class JumpSpawner : MonoBehaviour
{
    public GameObject Jump;
    public float spawnDelay;
    public GameObject aviso;
    public float avisoDelay;

    bool canSpawn; 

    // Start is called before the first frame update
    void Start()
    {
        canSpawn = true;
        aviso.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(canSpawn)
        {
            StartCoroutine("SpawnJump");
            //StartCoroutine("avisoImg");
        }
    }

    IEnumerator SpawnJump()
    {
        StartCoroutine("avisoImg");
        
        Instantiate(Jump, transform.position, Quaternion.identity);
        canSpawn = false;
        yield return new WaitForSeconds(spawnDelay);
        canSpawn = true;
    }

    public void stop()
    {
        canSpawn = false;     
        StopCoroutine("SpawnJump");
    }

    IEnumerator avisoImg()
    {
        aviso.SetActive(true);
        //Turn the Game Oject back off after 1 sec.
        yield return new WaitForSeconds(0.5f);
        //Game object will turn off
        aviso.SetActive(false);
    }
}

