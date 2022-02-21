using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletCtrl : MonoBehaviour {

    public Vector2 velocity;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
            rb.velocity = velocity;      
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        //if (other.gameObject.CompareTag("Player_Bullet"))
        //{
        //    //Destroy(gameObject);
        //    Debug.Log("Choca");
        //}
    }
}
//(other.gameObject.tag == "Enemy_Bullet")