using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyKamiAI : MonoBehaviour
{
	public float destroyKami;
	public float kamiSpeed;

    public Transform dustParticlePos;

    public void ActivateKami(Vector3 playerPos)
    {
        transform.DOMove(playerPos, kamiSpeed, false);
        SFXCtrl.instance.ShowPlayerLanding(dustParticlePos.position); //humo cuando se mueve
    }


    private void OnTriggerEnter2D(Collider2D other)
    //void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player"))

        {
            Destroy(gameObject, destroyKami);
            SFXCtrl.instance.EnemyExplosion(other.gameObject.transform.position);
        }

        if (other.gameObject.CompareTag("Player_Bullet"))
        {
            Debug.Log("pego la bala");          
            Destroy(gameObject);
            SFXCtrl.instance.EnemyExplosion(other.gameObject.transform.position);
            GameCtrl.instance.UpdateKills();
        }
    }

    //void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Player_Bullet"))
    //    {
    //        Destroy(gameObject, destroyKami);
    //        Debug.Log("pego la bala");
    //    }
    //}

    //public void stop(Vector3 playerPos)
    //{
    //    transform.DOMove(playerPos, kamiSpeed, true);
    //}

}
