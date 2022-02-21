using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Descuento nivel de vida del enemigo
/// cuando llega a cero lo destruyo
/// </summary>
public class Mod2Ctrl : MonoBehaviour {

    public float Mod1Live;

        void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player_Bullet"))
        {
            Mod1Live -= 1;
        }

        if(Mod1Live ==0)
        {
            GameCtrl.instance.BulletHitEnemy(other.gameObject.transform);
            Destroy(gameObject);
            GameCtrl.instance.UpdateKills();
        }
    }
}
