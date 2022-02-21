using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Movimiento del Enemigo
/// Chequear eventos de colision 
/// </summary>

public class SimpleMovement : MonoBehaviour {

    public static SimpleMovement instance;
    public float speed; //velocidad de movimiento

    Rigidbody2D rb;
    SpriteRenderer sr;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        SetStartingDirection();
    }
	

	void Update ()
    {
        Move();
	}

    public void EnemyStop()
    {
        speed = 0;
    }

    void Move()
    {
        Vector2 temp = rb.velocity;
        temp.x = speed;
        rb.velocity = temp;
    }

    void SetStartingDirection()
    {
        if(speed > 0)
        {
            sr.flipX = true;
        }
        else if(speed < 0)
        {
            sr.flipX = false;
        }
    }

    void FlipOnCollision()
    {
        speed = -speed;
        SetStartingDirection();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            FlipOnCollision();
        }
        if(other.gameObject.CompareTag("Player"))
        {
            speed = 0;
        }
    }
}
