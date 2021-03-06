using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI de enemigo (patrulla)
/// </summary>
public class EnemyPatrol : MonoBehaviour {

    public Transform leftBound, rightBound;
    public float speed;
    public float maxDelay, minDelay;

    bool canTurn;
    float originalSpeed;
    Rigidbody2D rb;
    SpriteRenderer sr;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        SetStartingDirection();
        canTurn = true;
    }
	
	void Update ()
    {
        Move();
        FlipOnEdges();
		
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            speed = 0;
        }
    }
}
