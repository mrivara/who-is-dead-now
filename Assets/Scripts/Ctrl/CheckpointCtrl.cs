using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCtrl : MonoBehaviour
{
    public BossOneIA BossOneIA;
    public PlayerCtrl Player;
    public CameraCtrl cam;
    public UI ui;
    public savePoint SavePoint;
    public GameObject Enemigos;


    void OnTriggerEnter2D(Collider2D other)
    {      
        if (other.gameObject.CompareTag("Player"))
        {
            GameCtrl.instance.save = true;
            cam.bossCamera = true;
            //SavePoint.DeleteSave();
            
            if (!GameCtrl.instance.data.BossActive)
            {
                AudioCtrl.instance.StopMusic();
                AudioCtrl.instance.MusicHumanoBoss();
                //activo mensaje
                ui.bossBattle.gameObject.SetActive(true);
                PlayerPrefs.SetFloat("CPX", 152);
                PlayerPrefs.SetFloat("CPY", other.gameObject.transform.position.y);
                Player.canMove = false;
                Player.canFire = false;
                Player.canJump = false;
                StartCoroutine(SetCountText());
                StartCoroutine(SetCountBB());
                Enemigos.SetActive(false);
            }
            else
            {
                AudioCtrl.instance.StopMusic();
                AudioCtrl.instance.MusicHumanoBoss();
                PlayerPrefs.SetFloat("CPX", 152);
                PlayerPrefs.SetFloat("CPY", other.gameObject.transform.position.y);
                BossOneIA.active = true;
                Enemigos.SetActive(false);
            }


        }
    }

    IEnumerator SetCountText()
    {
        {
            yield return new WaitForSeconds(5);
            //Time.timeScale = 1;
            //AudioCtrl.instance.StopMusic();
            Player.MobileStop();
            Player.canMove = true;
            Player.canFire = true;
            Player.canJump = true;
            GameCtrl.instance.UpdateBoss();
            BossOneIA.active = true;
        }
    }


    IEnumerator SetCountBB()
    {
        {
            yield return new WaitForSeconds(4);
            ui.bossBattle.gameObject.SetActive(false);
            //AudioCtrl.instance.StopMusic();
        }
    }


}
