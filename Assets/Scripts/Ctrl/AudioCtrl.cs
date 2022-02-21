using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioCtrl : MonoBehaviour {

    //public GameObject BGMusic;
    public static AudioCtrl instance;
    public PlayerAudio playerAudio;
    public AudioFX audioFX;
    public Transform player;

    public bool soundOn;
    public GameObject BGMusic;
    public GameObject BGMusicEspectro;
    public GameObject BGMusicBoss;
    public GameObject BGMusicEspectroBoss;
    public bool bgMusicOn;
    public GameObject btnSound, btnMusic;
    public Sprite imgSoundOn, imgSoundOff;
    public Sprite imgMusicOn, imgMusicOff;

    // Use this for initialization
    void Start()
    {
        if (instance == null)
            instance = this;

        if(DataCtrl.instance.data.playMusic)
        {
            BGMusic.SetActive(true);
            BGMusicEspectro.SetActive(true);
            btnMusic.GetComponent<Image>().sprite = imgMusicOn;
        }
        else
        {
            BGMusic.SetActive(false);
            BGMusicEspectro.SetActive(false);
            btnMusic.GetComponent<Image>().sprite = imgMusicOff;
        }

        if (DataCtrl.instance.data.playSound)
        {
            soundOn = true;
            btnSound.GetComponent<Image>().sprite = imgSoundOn;
        }
        else
        {
            soundOn = false;
            btnSound.GetComponent<Image>().sprite = imgSoundOff;
        }
    }

    public void FireBullets(Vector3 playerPos)
    {
        float volume = 0.3f;
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.fireBullets, playerPos,volume);
        }
    }

    public void CoinPickup(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.coinPickup, playerPos);
        }
    }

    public void EnemyDead(Vector3 playerPos)
    {
        float volume = 1.0f;
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.enemyDead, playerPos, volume);
        }
    }

    public void Weapload (Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.weapload, playerPos);
        }
    }

    public void OutOfAmmo(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.outOfAmmo, playerPos);
        }
    }

    public void WaterSplash(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.waterSplash, playerPos);
        }
    }

    public void PlayerDied(Vector3 playerPos)
    {
        if (soundOn)
        {
            AudioSource.PlayClipAtPoint(playerAudio.playerDied, playerPos);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ToggleSound()
    {
        if (DataCtrl.instance.data.playSound)
        {
            soundOn = false;
            btnSound.GetComponent<Image>().sprite = imgSoundOff;
            DataCtrl.instance.data.playSound = false;
        }
        else
        {
            soundOn = true;
            btnSound.GetComponent<Image>().sprite = imgSoundOn;
            DataCtrl.instance.data.playSound = true;
        }
    }

    public void ToggleMusic()
    {
        if(DataCtrl.instance.data.playMusic)
        {
            BGMusic.SetActive(false);
            BGMusicEspectro.SetActive(false);
            btnMusic.GetComponent<Image>().sprite = imgMusicOff;
            DataCtrl.instance.data.playMusic = false;
        }
        else
        {
            BGMusic.SetActive(true);
            BGMusicEspectro.SetActive(true);
            btnMusic.GetComponent<Image>().sprite = imgMusicOn;
            DataCtrl.instance.data.playMusic = true;
        }
    }

    public void MusicEspectro()
        {
         BGMusic.GetComponent<AudioSource>().volume = 0.0f;
         BGMusicEspectro.GetComponent<AudioSource>().volume = 0.3f;
        }
    public void MusicHumano()
        {
         BGMusic.GetComponent<AudioSource>().volume = 0.3f;
         BGMusicEspectro.GetComponent<AudioSource>().volume = 0.0f;
        }

    public void StopMusic()
    {
        BGMusic.GetComponent<AudioSource>().Stop();
        BGMusicEspectro.GetComponent<AudioSource>().Stop();
    }

    public void MusicHumanoBoss()
    {
        BGMusicBoss.GetComponent<AudioSource>().Play();
    }

}
