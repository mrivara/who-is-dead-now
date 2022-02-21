using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod4IA : MonoBehaviour
{

    public float jumpSpeed;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        EnemyJump();
    }

    // Update is called once per frame
    void Update()
    {
        //if (rb.velocity.y < 0)
        //{
        //    anim.SetInteger("State", 1);
        //}
    }

    public void EnemyJump()
    {
        rb.AddForce(new Vector2(0, jumpSpeed));
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))

        {
            SFXCtrl.instance.EnemyExplosion(other.gameObject.transform.position);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player_Bullet"))
        {
            Debug.Log("pego la bala");
            Destroy(gameObject);
            //SFXCtrl.instance.EnemyExplosion(other.gameObject.transform.position);
            GameCtrl.instance.UpdateKills();
        }

    }

}
