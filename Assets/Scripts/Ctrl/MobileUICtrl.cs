using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MobileUICtrl : MonoBehaviour {

    public GameObject player;
    public GameObject Espectro;

    PlayerCtrl playerCtrl;

    //Animator anim;
    //nuevo

    void Start () {
        playerCtrl = player.GetComponent<PlayerCtrl>();  
    }

    void Update()
    {
        if(player.activeSelf)
            {
            playerCtrl = player.GetComponent<PlayerCtrl>();  
            }
        else
            {
             playerCtrl = Espectro.GetComponent<PlayerCtrl>();  
            }
    }

    public void MobileMoveLeft()
    {
        playerCtrl.MobileMoveLeft();
    }

    public void MobileMoveRight()
    {
        playerCtrl.MobileMoveRight();
    }

    public void MobileMoveStop()
    {
        playerCtrl.MobileStop();
    }

    public void MobileJump()
    {
        playerCtrl.MobileJump();
    }

    public void MobileTransform()
    {
        playerCtrl.Transformacion();
    }
}
