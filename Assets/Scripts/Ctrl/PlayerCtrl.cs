using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Player controler
/// </summary>
/// Bug: controles, si dejas apretado el boton cuando moris se sigue moviendo.

public class PlayerCtrl : MonoBehaviour {

    public GameObject Humano;
    public GameObject Espectro;
    public float life;
    public Slider PlayerHealth;
    
    //transformacion    
    //public Slider sliderTransform;
    //public float mana;
    //public int costoMana;

    //fin transformacion
    public int speedBoost;
    public float jumpSpeed; //set 600
    public bool isGrounded;
    public Transform feet;
    public float feetRadius;
    public LayerMask whatIsGround;
    public float boxWidth;
    public float boxHeight;
    public float delayForDoubleJump;
    public Transform leftBulletSpawnPos, rightBulletSpawnPos;
    public GameObject leftBullet, rightBullet;
    public bool canFire, canMove, canJump, canTransform;
    public bool hurt;
    public UI ui;
    public static PlayerCtrl instance; 

    public EnemyPatrolMod2 EnemyMod2;
    public BossOneIA BossOneIA;
    public JumpSpawner JumpSpawner;
    //public EnemyKamiAI KamiIA;

    public int EstadoPlayer;
    public bool SFXOn;
    public GameObject garbageCtrl;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    bool isJumping, canDoubleJump;
    bool leftPressed, rightPressed;


    protected JoyButton joyButton;
    bool Fire;

    public scrSprite ScrSprite;
    /// <summary>
    /// agregado para ver si soluciono el problema de la animacion
    /// </summary>
    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;
    public bool isGhost;
    public GameObject Ghost;

    private void Awake()
    {
        if(PlayerPrefs.HasKey("CPX"))
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("CPX"), PlayerPrefs.GetFloat("CPY"), transform.position.z);
        }
    }


    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //nuevo
        //le paso la animacion de humano       
        anim.runtimeAnimatorController = anim1 as RuntimeAnimatorController;

        joyButton = FindObjectOfType<JoyButton>();
        canFire = true;
        canJump = true;
        canMove = true;
        canTransform = true;
        
        //bara de transformacion
        //sliderTransform.GetComponent<Slider>().value = 0.0f;
        //StartCoroutine(tiempoTransform());
        life = PlayerHealth.GetComponent<Slider>().value;
        
	}
	
	void Update () {

         
        isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(boxWidth,boxHeight ), 360.0f, whatIsGround);        
        //nuevo

        float playerSpeed = Input.GetAxisRaw("Horizontal");
        playerSpeed *= speedBoost;
        if (playerSpeed != 0)
            MoveHorizontal(playerSpeed);
        else
            StopMoving();

        if (Input.GetButtonDown("Jump"))
            Jump();

        ShowFalling();

        if (leftPressed)
            MoveHorizontal(-speedBoost);
        if (rightPressed)
            MoveHorizontal(speedBoost);

        if (!Fire && (joyButton.Pressed || Input.GetButtonDown("Fire1")))
        {
            FireBullets();
            joyButton.Pressed = false;
        }

        if(canTransform && Input.GetButtonDown("Fire2"))
        {
            life = PlayerHealth.GetComponent<Slider>().value;
            PlayerHealth.GetComponent<Slider>().value = life;
            Transformacion();
        }

        if(hurt == true)
        {
            MobileStop();
            anim.SetInteger("State", -1);
            canFire = false;
            canJump = false;
            canMove = false;
            canTransform = false;
        }        
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(feet.position, new Vector3(boxWidth, boxHeight, 0));
    }

    void MoveHorizontal(float playerSpeed)
    {
        if(canMove)
        {
            rb.velocity = new Vector2(playerSpeed, rb.velocity.y);

            if (playerSpeed < 0)
                sr.flipX = true;
            else if (playerSpeed > 0)
                sr.flipX = false;
            if (!isJumping)
                anim.SetInteger("State", 1);
        }
    }

    void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (!isJumping)
            anim.SetInteger("State", 0);
    }

    void ShowFalling()
    {
        if (rb.velocity.y < 0)
        {
            anim.SetInteger("State", 3);
        }
    }

    void Jump()
    {
        if(canJump)
        {
            if (isGrounded)
            {
                isJumping = true;
                rb.AddForce(new Vector2(0, jumpSpeed));
                anim.SetInteger("State", 2);

                Invoke("EnableDoubleJump", delayForDoubleJump);
            }

            if (canDoubleJump && !isGrounded)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(0, jumpSpeed));
                anim.SetInteger("State", 2);

                canDoubleJump = false;
            }
        }
    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
            isJumping = false;
        //daño a PJ
        if (other.gameObject.tag == "Enemy" && !hurt)
        //if (other.gameObject.tag == "Enemy" && life > 1)
        {
            GameCtrl.instance.PlayerDiedAnimation(gameObject);
            AudioCtrl.instance.PlayerDied(gameObject.transform.position);
            hurt = true;
            EnemyMod2.stop();
            BossOneIA.stop();
            //JumpSpawner.stop();
            //JumpSpawner.StopCoroutine("SpawnJump"); //comentar porque en este juego no hay este objeto
        }
        if (other.gameObject.tag == "Enemy_Fly" && !hurt)
        {
            GameCtrl.instance.PlayerDiedAnimation(gameObject);
            AudioCtrl.instance.PlayerDied(gameObject.transform.position);
            hurt = true;
            JumpSpawner.StopCoroutine("SpawnJump");
        }
        if (other.gameObject.tag == "Enemy_Bullet" && !hurt && BossOneIA.live)
        {
            GameCtrl.instance.PlayerDiedAnimation(gameObject);
            AudioCtrl.instance.PlayerDied(gameObject.transform.position);
            hurt = true;
            EnemyMod2.stop();
            BossOneIA.stop();
            //JumpSpawner.StopCoroutine("SpawnJump"); //comentar porque en este juego no hay este objeto

        }
#region Boss

        if (other.gameObject.tag == "Boss" && !hurt)
        {
            GameCtrl.instance.PlayerDiedAnimation(gameObject);
            AudioCtrl.instance.PlayerDied(gameObject.transform.position);
            hurt = true;
            BossOneIA.stop();
            PlayerHealth.value = 0;
        }

        if (other.gameObject.tag == "Boss_Bullet" && !hurt && BossOneIA.live && life ==1)
        {
            PlayerHealth.value = 0;
            GameCtrl.instance.PlayerDiedAnimation(gameObject);
            AudioCtrl.instance.PlayerDied(gameObject.transform.position);
            hurt = true;
            EnemyMod2.stop();
            BossOneIA.stop();
            JumpSpawner.StopCoroutine("SpawnJump");
        }

        if (other.gameObject.tag == "Boss_Bullet" && !hurt && BossOneIA.live && life > 1)
        {
            life = life - 1;
            PlayerHealth.value = (float)life;         
            sr.color = Color.red;
            Invoke("RestoreColor", 0.1f);
        }

        #endregion Boss

#region Boss quieta vida
        // Si queremos que al tocar el enemigo el player pierda vida
        //if (other.gameObject.tag == "Boss" && !hurt && life == 1)
        //{
        //    GameCtrl.instance.PlayerDiedAnimation(gameObject);
        //    AudioCtrl.instance.PlayerDied(gameObject.transform.position);
        //    hurt = true;
        //    BossOneIA.stop();
        //}
        //if (other.gameObject.tag == "Boss" && !hurt && life > 1)
        //{
        //    //GameCtrl.instance.PlayerDiedAnimation(gameObject);
        //    //AudioCtrl.instance.PlayerDied(gameObject.transform.position);
        //    //hurt = true;
        //    //BossOneIA.stop();
        //    life = life - 1;
        //}
        //fin daño
        #endregion

    }

    void FireBullets()
    {

        if(canFire)
        {
            if (GameCtrl.instance.data.ammo > 0)
            {
                if (anim.GetInteger("State") == 0)
                { anim.SetInteger("State", 4); }

                if (sr.flipX)
                {

                    Instantiate(leftBullet, leftBulletSpawnPos.position, Quaternion.identity);
                }
                if (!sr.flipX)
                {
                    Instantiate(rightBullet, rightBulletSpawnPos.position, Quaternion.identity);
                }
                GameCtrl.instance.DeleteAmmo();
                AudioCtrl.instance.FireBullets(gameObject.transform.position);
            }
            if (GameCtrl.instance.data.ammo == 0)
            {
                AudioCtrl.instance.OutOfAmmo(gameObject.transform.position);
                if (anim.GetInteger("State") == 0)
                { anim.SetInteger("State", 4); }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Coin":
                if (SFXOn)
                {
                    SFXCtrl.instance.ShowCoinSparkle(other.gameObject.transform.position);
                    GameCtrl.instance.UpdateCoinCount();
                    AudioCtrl.instance.CoinPickup(gameObject.transform.position);
                }
                break;
            case "Checkpoint":
                if (SFXOn)
                {
                   // GameCtrl.instance.UpdateBoss(); //saque esto y lo meti en update
                }
                break;
            case "Water":
                //animacion de agua al caer
                canFire = false;
                canMove = false;
                canJump = false;

                garbageCtrl.SetActive(false);
                SFXCtrl.instance.ShowSplash(gameObject.transform.position);
                AudioCtrl.instance.WaterSplash(gameObject.transform.position);
                //info the GameCtrl
                GameCtrl.instance.PlayerDrowned(other.gameObject);
                break;
            case "Powerup_Bullet":
                //canFire = true;
                GameCtrl.instance.UpdateAmmoCount();
                Vector3 powerupPos = other.gameObject.transform.position;
                AudioCtrl.instance.Weapload(gameObject.transform.position);
                //Destroy(other.gameObject);
                if (SFXOn)
                    SFXCtrl.instance.ShowBulletSparkle(powerupPos);
                break;
            case "Enemy":
                if(!hurt)
                {
                    canFire = false;
                    canMove = false;
                    canJump = false;
                    JumpSpawner.stop();
                    JumpSpawner.StopCoroutine("SpawnJump"); //rutina de jump
                    EnemyMod2.stop();
                    BossOneIA.stop();
                    GameCtrl.instance.PlayerDiedAnimation(gameObject);
                    AudioCtrl.instance.PlayerDied(gameObject.transform.position);
                    hurt = true;
                }

                hurt = true;
                break;
            case "Enemy_Fly":
                if (!hurt)
                {
                    canFire = false;
                    canMove = false;
                    canJump = false;
                    GameCtrl.instance.PlayerDiedAnimation(gameObject);
                    AudioCtrl.instance.PlayerDied(gameObject.transform.position);
                    //JumpSpawner.StopCoroutine("SpawnJump");
                    hurt = true;
                }

                break;
            default:
                break;
        }
    }

    public void Transformacion()
    {    
        //poner if si esta apretado el boton 
     if(canTransform)
            {
            if(isGhost == false)
     {
         GameCtrl.instance.BWFondosOn();
         AudioCtrl.instance.MusicEspectro();
         Ghost.SetActive (true);    
         isGhost = true;
         anim.runtimeAnimatorController = anim2 as RuntimeAnimatorController;
         rightBulletSpawnPos.position = new Vector3(rightBulletSpawnPos.position.x, (rightBulletSpawnPos.position.y - 0.3f), rightBulletSpawnPos.position.z);
         leftBulletSpawnPos.position = new Vector3(leftBulletSpawnPos.position.x, (leftBulletSpawnPos.position.y - 0.3f), leftBulletSpawnPos.position.z);
      }

     else
     if (isGhost == true)
     {
         GameCtrl.instance.BWFondosOff();
         AudioCtrl.instance.MusicHumano();
         Ghost.SetActive (false);   
         isGhost = false;
         anim.runtimeAnimatorController = anim1 as RuntimeAnimatorController;
         rightBulletSpawnPos.position = new Vector3(rightBulletSpawnPos.position.x, (rightBulletSpawnPos.position.y + 0.3f), rightBulletSpawnPos.position.z);
         leftBulletSpawnPos.position = new Vector3(leftBulletSpawnPos.position.x, (leftBulletSpawnPos.position.y + 0.3f), leftBulletSpawnPos.position.z);

            }
            }
        }


    public void StopPlayer()
    {
        canMove = false;
        canJump = false;
        canFire = false;
        canTransform = false;
    }

    void RestoreColor()
    {
        sr.color = Color.white;
    }

    #region Controles_Celu 
    //mobileUI
    public void MobileMoveLeft()
    {
        leftPressed = true;
    }

    public void MobileMoveRight()
    {
        rightPressed = true;
    }

    public void MobileStop()
    {
        leftPressed = false;
        rightPressed = false;

        StopMoving();
    }

    public void MobileJump()
    {
        Jump();
    }
}
#endregion