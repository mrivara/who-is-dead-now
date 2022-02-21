using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSprite : MonoBehaviour

{
    public SpriteRenderer spriteRenderer;
    public Sprite colorSprite;
    public Sprite bwSprite;
    public GameObject isGhost;
    //public GameObject Espectro;

   void cambiarBW()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = bwSprite;
    }

    void cambiarColor()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = colorSprite;
    }

    void Update ()
    {
        if (isGhost.activeSelf)
        {
            cambiarBW();
        }
        else
        {
            cambiarColor();
        }


    }

}
