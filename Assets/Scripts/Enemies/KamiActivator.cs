using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamiActivator : MonoBehaviour
{

    public GameObject kami;
    EnemyKamiAI kai;


    // Start is called before the first frame update
    void Start()
    {
        kai = kami.GetComponent<EnemyKamiAI>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            kai.ActivateKami(other.gameObject.transform.position);
        }
    }
}
