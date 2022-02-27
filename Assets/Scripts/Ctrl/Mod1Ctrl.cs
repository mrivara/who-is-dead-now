using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Descuento nivel de vida del enemigo
/// cuando llega a cero lo destruyo
/// </summary>
public class Mod1Ctrl : MonoBehaviour {

    public float Mod1Live;
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player_Bullet"))
        {
            Mod1Live -= 1;
            sr.color = Color.red;
            Invoke("RestoreColor", 0.1f);
        }

        if(Mod1Live ==0)
        {
            GameCtrl.instance.BulletHitEnemy(other.gameObject.transform);
            Destroy(gameObject);
            //poner aca para que de puntos
            GameCtrl.instance.UpdateKills();
        }
    }

    void RestoreColor()
    {
        sr.color = Color.white;
    }


}
