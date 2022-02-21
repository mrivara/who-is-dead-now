using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI de enemigo (patrulla)
/// AI con disparo
/// </summary>
public class EnemyPatrolMod2 : MonoBehaviour {

    public Transform leftBound, rightBound;
    public float speed;
    public float maxDelay, minDelay;
    public float maxDelayShot, minDelayShot;
    public Transform leftBulletSpawnPos, rightBulletSpawnPos;
    public GameObject leftBullet, rightBullet;
    public bool canFire;
    public bool canReload;

    bool canTurn;
    float originalSpeed;
    Rigidbody2D rb;
    SpriteRenderer sr;

    void Start ()
    {
        leftBullet.gameObject.SetActive(true);
        rightBullet.gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        SetStartingDirection();
        canTurn = true;
        //nuevo BOSS
        canReload = true;
        Invoke("Reload", Random.Range(minDelayShot, maxDelayShot));
    }
	
	void Update ()
    {
        Move();
        FlipOnEdges();
        //Nuevo
        if (canFire )
        {
            FireBulletsMod2();
            canFire = false;
        }
        //Fin
    }

    void SetStartingDirection()
    {
        if (speed > 0)
            sr.flipX = true;
        else if (speed < 0)
            sr.flipX = false;
    }

    void Move()
    {
        Vector2 temp = rb.velocity;
        temp.x = speed;
        rb.velocity = temp;
    }

    void FlipOnEdges()
    {
        if(sr.flipX && transform.position.x >= rightBound.position.x)
        {
            if(canTurn )
            {
                canTurn = false;
                originalSpeed = speed;
                speed = 0;
                StartCoroutine("TurnLeft", originalSpeed);
            }
        }
        else if (!sr.flipX && transform.position.x <= leftBound.position.x)
        {
            if (canTurn)
            {
                canTurn = false;
                originalSpeed = speed;
                speed = 0;
                StartCoroutine("TurnRight", originalSpeed);
            }
        }
    }

    IEnumerator TurnLeft(float originalSpeed)
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        sr.flipX = false;
        speed = -originalSpeed;
        canTurn = true;
    }

    IEnumerator TurnRight(float originalSpeed)
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        sr.flipX = true;
        speed = -originalSpeed;
        canTurn = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(leftBound.position, rightBound.position);
    }

    //Nuevo BOSS
    void Reload()
    {
        if(canReload)
        {
            canFire = true;
        }        
    }

    //metodo de disparo
    void FireBulletsMod2()
    {
            if (sr.flipX)
            {
                Instantiate(rightBullet, rightBulletSpawnPos.position, Quaternion.identity);
                Invoke("Reload", maxDelayShot);
            }
            if (!sr.flipX)
            {
                Instantiate(leftBullet, leftBulletSpawnPos.position, Quaternion.identity);
                Invoke("Reload", maxDelayShot);
            }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            speed = 0;
            stop();
        }
    }

   public  void stop()
    {
        leftBullet.gameObject.SetActive(false);
        rightBullet.gameObject.SetActive(false);
        canFire = false;
    }
}
