using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXCtrl : MonoBehaviour {

    public static SFXCtrl instance;
    public SFX sfx;


    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ShowCoinSparkle(Vector3 pos)
    {
        Instantiate(sfx.sfx_coin_pickup, pos, Quaternion.identity);
    }

    public void EnemyExplosion(Vector3 pos)
    {
        Instantiate(sfx.sfx_enemy_explosion, pos, Quaternion.identity);
    }

    public void ShowBulletSparkle(Vector3 pos)
    {
        Instantiate(sfx.sfx_bullet_pickup, pos, Quaternion.identity);
    }

    public void ShowPlayerLanding(Vector3 pos)
    {
        Instantiate(sfx.sfx_playerLands, pos, Quaternion.identity);
    }

    public void ShowSplash(Vector3 pos)
    {
        Instantiate(sfx.sfx_splash, pos, Quaternion.identity);
    }

    public void ShowEnemyLanding(Vector3 pos)
    {
        Instantiate(sfx.sfx_enemyLands, pos, Quaternion.identity);
    }
    
}
