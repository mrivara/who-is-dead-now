using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCtrl : MonoBehaviour {

    public enum AmmoFX
    {
        Vanish,
        Fly
    }

    public AmmoFX ammoFX;
    public float speed;
    public bool startFlying;

    GameObject coinMeter;

    void Start()
    {
        startFlying = false;
        if (ammoFX == AmmoFX.Fly)
        {
            coinMeter = GameObject.Find("img_Ammo");
        }

    }

    void Update()
    {
        if (startFlying)
        {
            transform.position = Vector3.Lerp(transform.position, coinMeter.transform.position, speed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (ammoFX == AmmoFX.Vanish)
                Destroy(gameObject);
            else if (ammoFX == AmmoFX.Fly)
            {
                gameObject.layer = 0;
                startFlying = true;
            }
        }

        if(other.gameObject.CompareTag("Ground"))
        {
            Destroy(other.gameObject);
        }
    }
}
