using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCtrl : MonoBehaviour {

    public Vector2 velocity;
    Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}	

	void Update () {
        rb.velocity = velocity;
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }

        // else if (other.gameObject.CompareTag("Enemy_Bullet"))
        //{
        //    Destroy(other.gameObject);
        //    Debug.Log("Choca");
        //}
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameCtrl.instance.BulletHitEnemy(other.gameObject.transform);
            Destroy(gameObject);
        }
    }
}
