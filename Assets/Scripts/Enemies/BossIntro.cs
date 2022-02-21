using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIntro : MonoBehaviour

{

    public PlayerCtrl Player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.canMove = false;
            Player.canFire = false;
            Player.canJump = false;
        }
    }


}
