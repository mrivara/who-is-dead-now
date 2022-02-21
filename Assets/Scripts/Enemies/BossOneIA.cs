using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossOneIA : MonoBehaviour
{
    public Transform leftBound, rightBound;
    public float speed;
    public float maxDelay, minDelay;
    public float maxDelayShot, minDelayShot;
    public float maxDelayBoom, minDelayBoom; //bomba
    public Transform bulletSpawn1, bulletSpawn2;
    public GameObject bullet1, bullet2;
    public bool canFire, canBoom;
    public bool active, live;
    public bool canReload, canReloadBoom;
    public bool dire;
    /// vida del enemigo
    public int health;
    public Slider bossHealth;
    //
    bool canTurn;

    float originalSpeed;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public Transform dustParticlePos;

    void Start()
    {
        bullet1.gameObject.SetActive(true);
        bullet2.gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        //SetStartingDirection();
        canTurn = true;
        //nuevo BOSS
        canReload = true;
        canReloadBoom = true;
        Invoke("Reload", Random.Range(minDelayShot, maxDelayShot));
        Invoke("Reload2", Random.Range(minDelayBoom, maxDelayBoom));
        live = true;
    }

    void Update()
    {
        if (active)
        {
            Move();
            FlipOnEdges();
            if (speed != 0)
            {
                SFXCtrl.instance.ShowPlayerLanding(dustParticlePos.position);
            }
        }

        //Nuevo
        //arma secundaria
        if (health == 0)
        {
            live = false;
            GameCtrl.instance.BulletHitBoss(gameObject.transform);
        }

        if (canFire && active)
        {
            FireBulletsMod();
            canFire = false;
        }       
        //bomba
        if (canBoom && health <4)
        {
                FireBulletsBoom();
                canBoom = false;
        }
 
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
        //SFXCtrl.instance.ShowPlayerLanding(dustParticlePos.position);
    }

    void FlipOnEdges()
    {
        if (dire && transform.position.x >= rightBound.position.x)
        {
            if (canTurn)
            {
                canTurn = false;
                originalSpeed = speed;

                speed = 0;
                StartCoroutine("TurnLeft", originalSpeed);
            }
        }
        else if (!dire && transform.position.x <= leftBound.position.x)
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
        dire= false;
        speed = -originalSpeed;
        canTurn = true;
    }

    IEnumerator TurnRight(float originalSpeed)
    {
        yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        dire = true;
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
        if (canReload)
        {
            canFire = true;
        }
    }

    void Reload2()
    {
        if (canReloadBoom)
        {
            canBoom = true;
        }
    }

    //metodo de disparo
    void FireBulletsMod()
    {
        Instantiate(bullet2, bulletSpawn2.position, Quaternion.identity);
        Invoke("Reload", maxDelayShot);
    }
    //bomba
    void FireBulletsBoom()
    {
        Instantiate(bullet1, bulletSpawn1.position, Quaternion.identity);
        Invoke("Reload2", maxDelayBoom);
    }


    void RestoreColor()
    {
        sr.color = Color.white;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            speed = 0;
            stop();
        }

        if (other.gameObject.CompareTag("Player_Bullet"))
        {
            if (health == 0)
            {
                live = false;
                GameCtrl.instance.BulletHitBoss(gameObject.transform);
                //destruir balas
            }
               
            if (health > 0 )
            {
                health--;
                bossHealth.value = (float)health;
                sr.color = Color.red;
                Invoke("RestoreColor", 0.1f);
            }
        }
    }

    public void stop()
    {
        bullet1.gameObject.SetActive(false);
        bullet2.gameObject.SetActive(false);
        canFire = false;
        canBoom = false;
        speed = 0;
    }

}
