using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using DG.Tweening;




public class GameCtrl : MonoBehaviour
{
    public static GameCtrl instance;
    public float restartDelay;

    //[HideInInspector]
    public GameData data;
    public UI ui;
    public PlayerCtrl player;


    //public Text txtCoinCount; //para asignar las monedas 
    //public Text txtScore;
    public int coinValue;
    public int enemieValue;
    public bool save;
    public GameObject ammoPlus;

    public bool barra;

    string dataFilePath;
    BinaryFormatter bf;
    bool isPaused;
    public bool isBack;
    private GameObject[] gameObjects;

    public GameObject Humano;
    public GameObject Espectro;
    
    //Fondo Negro
    public GameObject BG_BW_0;
    public GameObject BG_BW_1;
    public GameObject BG_BW_2;
    public GameObject BG_BW_3;
    public GameObject BG_BW_4;


    void Awake()
    {
        if (instance == null)
            instance = this;

        bf = new BinaryFormatter();
        dataFilePath = Application.persistentDataPath + "/game.dat";
        Debug.Log(dataFilePath);

    }

    public void Start()
    {
        Humano.SetActive(true);
        //Espectro.SetActive(true);
             

        DataCtrl.instance.RefreshData();
        data = DataCtrl.instance.data;
        
        HandleFirstBoot();
        menuLVL();
        UpdateHearts();

        RefreshUI();

        ui.bossBattle.gameObject.SetActive(false);

        if (!data.BossActive)
        {
            ui.BossHealth.gameObject.SetActive(false);
            ui.PlayerHealth.gameObject.SetActive(false);
        }
        else
        {
            ui.BossHealth.gameObject.SetActive(true);
            ui.PlayerHealth.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (isPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Delete checkpoint
    /// </summary>
   public void DeleteCheckPoints()
    {
        save = false;
        PlayerPrefs.DeleteKey("CPX");
        PlayerPrefs.DeleteKey("CPY");
    }

    //activar fondos en negro
    public void BWFondosOn()
    {
        BG_BW_0.SetActive(true);
        BG_BW_1.SetActive(true);
        BG_BW_2.SetActive(true);
        BG_BW_3.SetActive(true);
        BG_BW_4.SetActive(true);
    }
    
    //apago findos en negro
    public void BWFondosOff()
    {
        BG_BW_0.SetActive(false);
        BG_BW_1.SetActive(false);
        BG_BW_2.SetActive(false);
        BG_BW_3.SetActive(false);
        BG_BW_4.SetActive(false);
    }

    #region DatosDelJuego

    public void RefreshUI()
    {
        if (File.Exists(dataFilePath))
        {

            ui.txtCoinCount.text = " x " + data.coinCount;
            ui.txtScore.text = "Score: " + data.score;
            ui.ammo.text = ": " + data.ammo;

        }
    }

    void OnEnable()
    {
        Debug.Log("Data Loaded");
        RefreshUI();
    }
    void OnDisable()
    {
        Debug.Log("Data Saved");
        //SaveData();
        DataCtrl.instance.SaveData(data);
        Time.timeScale = 1;
    }

    public int GetScore()
    {
        return data.score;
    }
    /// <summary>
    /// guardo el numero de estrellas ganadas
    /// </summary>
    public void SetStarsAwarded(int levelNumber, int numOfStars)
    {
        data.levelData[levelNumber].starsAwarded = numOfStars;
        //borrar
        //nuevo
        DataCtrl.instance.SaveData(data);
        Debug.Log("Numero de estrellas = " + data.levelData[levelNumber].starsAwarded);
        Debug.Log("level Number = " + data.levelData[levelNumber].levelNumber);
    }
    /// <summary>
    /// Desbloqueo el nivel
    /// </summary>
    /// <param name="levelNumber"></param>
    /// 
    public void UnlockLevel(int levelNumber)
    {
        if ((levelNumber + 1) <= (data.levelData.Length - 1))
            data.levelData[levelNumber + 1].isUnlocked = true;
        DataCtrl.instance.SaveData(data); //agregado 30112020
    }


    public void UpdateCoinCount()
    {
        data.coinCount += 1;
        ui.txtCoinCount.text = " x " + data.coinCount;
        UpdateScore(coinValue);

    }

    public void UpdateAmmoCount()
    {
        data.ammo += 5;
        ui.ammo.text = ": " + data.ammo;
    }

    public void UpdateKills()
    {
        data.kills += 1;
        UpdateScoreKill(enemieValue);
    }

    public void UpdateBoss()
    {
        data.BossActive = true;
        ui.BossHealth.gameObject.SetActive(true);
        ui.PlayerHealth.gameObject.SetActive(true);
    }

    public void DeleteAmmo()
    {
        data.ammo -= 1;
        ui.ammo.text = ": " + data.ammo;
    }

    public void UpdateScore(int value)
    {
        data.score += value;
        ui.txtScore.text = "Score: " + data.score;
    }

    public void UpdateScoreKill(int value)
    {
        data.score += value;
        ui.txtScore.text = "Score: " + data.score;
    }

    public void ScoreLVL()
    {
        Debug.Log("antes de calculo: "+ data.score);
        if (data.score > 0)
        {
            data.score = data.score * data.lives;

            Debug.Log("despues de calculo: " + data.score);
            Debug.Log("vidas: " + data.lives);
        }
        else
        {
            data.score = 0;
        } 

        ui.txtScore.text = "Score: " + data.score;
    }

    public void BulletHitEnemy(Transform enemy)
    {
        Vector3 pos = enemy.position;
        pos.z = 20f;
        //FX Dead
        AudioCtrl.instance.EnemyDead(pos);
        SFXCtrl.instance.EnemyExplosion(pos);
        //da item despues de morir
        Instantiate(ammoPlus, pos, Quaternion.identity);
        //destuir enemigo
        Destroy(enemy.gameObject, 2.0f);
        //update the score      
    }


    public void BulletHitBoss(Transform enemy)
    {
        //calculo score
        //SFX
        Vector3 pos = enemy.position;
        pos.z = 20f;
        SFXCtrl.instance.EnemyExplosion(pos);
        //da item despues de morir
        Instantiate(ammoPlus, pos, Quaternion.identity);
        //destuir enemigo
        DestroyAll("Enemy");
        DestroyAll("Enemy_Bullet");
        Destroy(enemy.gameObject);
        //update the score
        //aparece menu
        Invoke("ShowLevelCompleteMenu", 3f);
        //player.StopPlayer();

    }

    void DestroyAll(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject.Destroy(enemies[i]);
        }
    }

    //nuevo 2019
    public void ShowLevelCompleteMenu()
    {
        //update score
        ScoreLVL();
        GameCtrl.instance.LevelComplete();
    }

    public void LevelComplete()
    {
        ui.panelMobileUI.SetActive(false);
        ui.LevelCompleteMenu.SetActive(true);
        data.lvlControl = true;
        save = false;
    }
    //Fin Nuevo 2019

    #endregion
    /// <summary>
    /// Restar LVL
    /// </summary>
    /// 

    public void PlayerDied(GameObject player)
    {
        player.SetActive(false);  //esto hace que se destruya el PJ
        CheckLives();
    }

    public void PlayerDiedAnimation(GameObject player)
    {
        //SimpleMovement.instance.speed = 0;
        StartCoroutine("PauseBeforeReload", player);
        //anim.SetInteger("State", -1);
    }

    IEnumerator PauseBeforeReload(GameObject player)
    {
        yield return new WaitForSeconds(4f); //delay
        PlayerDied(player);
    }

    /// <summary>
    /// Restar LVL cuando cae en agua
    /// </summary>
    public void PlayerDrowned(GameObject player)
    {
        CheckLives();
    }

    void RestartLevel()
    {
        // carga la scena en curso, no se le pasa nombre 
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    void HandleFirstBoot()
    {
        if (data.isFirstBoot)
        {
            data.lives = 3; //cantidad de vidas
            data.coinCount = 0; //cantidad de monedas
            data.score = 0; //marcador en 0
            data.ammo = 5;
            data.kills = 0;
            data.BossActive = false; //activa la barra de energia del j
            data.isFirstBoot = false;
            save = false;
        }
    }


    void menuLVL()
    {
        if (data.lvlControl)
        {
            data.lives = 3; //cantidad de vidas
            data.coinCount = 0; //cantidad de monedas
            data.score = 0; //marcador en 0
            data.ammo = 5;
            data.kills = 0;
            data.BossActive = false; //activa la barra de energia del j
            data.lvlControl = false;
            save = false;
        }
    }

    void UpdateHearts()
    {
        if (data.lives == 3)
        {
            ui.heart1.sprite = ui.fullHeart;
            ui.heart2.sprite = ui.fullHeart;
            ui.heart3.sprite = ui.fullHeart;
        }
        if (data.lives == 2)
        {
            ui.heart1.sprite = ui.emptyHeart;
        }
        if (data.lives == 1)
        {
            ui.heart1.sprite = ui.emptyHeart;
            ui.heart2.sprite = ui.emptyHeart;
        }
    }
    //update vidas
    public void CheckLives()
    {
        {
            int updatedLives = data.lives;
            updatedLives -= 1;
            data.lives = updatedLives;

            if (data.lives == 0)
            {
                data.lives = 3;
                data.coinCount = 0; //cantidad de monedas
                data.score = 0; //marcador en 0
                data.ammo = 5;
                data.kills = 0;
                data.BossActive = false;
                save = false;
                //nuevo
                DataCtrl.instance.SaveData(data);

                Invoke("GameOver", restartDelay);
            }
            else
            {
                if (!save)
                {
                    //cargar datos en 0
                    //data.coinCount = 0; //cantidad de monedas
                    //data.score = 0; //marcador en 0
                    data.ammo = 5;
                    //data.kills = 0;
                    data.BossActive = false;
                    save = false;
                    //Fin datos en 0
                    DataCtrl.instance.SaveData(data);
                    Invoke("RestartLevel", restartDelay);
                }
                else
                {
                    DataCtrl.instance.SaveData(data);
                    Invoke("RestartLevel", restartDelay);
                }

            }
        }
    }

    void GameOver()
    {
        //ui.panelGameOver.SetActive(true);
        
        ui.panelGameOver.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 0.7f, false);
        DeleteCheckPoints();
    }

    public void Inicio()
    {
        data.lives = 3; //cantidad de vidas
        data.coinCount = 0; //cantidad de monedas
        data.score = 0; //marcador en 0
        data.ammo = 5;
        data.kills = 0;
        data.BossActive = false; //activa la barra de energia del j
        data.isFirstBoot = false;
        save = false;
        DeleteCheckPoints();
    }

    public void ShowPausePanel()
    {
        if (ui.panelMobileUI.activeInHierarchy)
            ui.panelMobileUI.SetActive(false);
        if (ui.panelHUD.activeInHierarchy)
            ui.panelHUD.SetActive(false);
        ui.panelPause.SetActive(true);

        ui.panelPause.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 0.7f, false);
        Invoke("SetPause", 0.8f);
    }
    public void HidePausePanel()
    {
        isPaused = false;

        if (!ui.panelMobileUI.activeInHierarchy)
            ui.panelMobileUI.SetActive(true);
        if (!ui.panelHUD.activeInHierarchy)
            ui.panelHUD.SetActive(true);

        //ui.panelPause.SetActive(false);
        //isPaused = false;
        ui.panelPause.gameObject.GetComponent<RectTransform>().DOAnchorPosY(600f, 0.7f, false);
    }

    void SetPause()
    {
        isPaused = true;
    }

    public void updateLVL()
    {
        data.lvlControl = true;
        //save = false;       
        DeleteCheckPoints();
    }
}
