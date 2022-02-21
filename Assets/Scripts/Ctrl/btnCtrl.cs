using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class btnCtrl : MonoBehaviour
{

    int levelNumber;
    Button btn;
    Image btnImg;
    //Text btnText;
    Transform star1, star2, star3;

    public Sprite lockedBtn;
    public Sprite unlockedBtn;
    public string sceneName;
    
    public GameData data;

    

    // Start is called before the first frame update
    void Start()
    {
        levelNumber = int.Parse(transform.gameObject.name);
        
        btn = transform.gameObject.GetComponent<Button>();
        btnImg = btn.GetComponent<Image>();
        //btnText = btn.gameObject.transform.GetChild(3).GetComponent<Text>();

        star1 = btn.gameObject.transform.GetChild(0);
        star2 = btn.gameObject.transform.GetChild(1);
        star3 = btn.gameObject.transform.GetChild(2);

        //star1.gameObject.SetActive(false);
        //star2.gameObject.SetActive(false);
        //star3.gameObject.SetActive(false);

        BtnStatus();
    }

    // Update is called once per frame
    void BtnStatus()
    {
        // bool unlocked = DataCtrl.instance.IsUnlocked(levelNumber);
        //int starsAwarded = DataCtrl.instance.getStars(levelNumber);
        bool unlocked = DataCtrl.instance.isUnlocked(levelNumber);
        int starsAwarded = DataCtrl.instance.getStars(levelNumber);
        Debug.Log("cantidad de estrellas: " + starsAwarded);

        #region Estrellas 

        if (unlocked)
        {
            //show appropriate number of stars
            if (starsAwarded == 3)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(true);
            }

            if (starsAwarded == 2)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(true);
                star3.gameObject.SetActive(false);
            }

            if (starsAwarded == 1)
            {
                star1.gameObject.SetActive(true);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
            }

            if (starsAwarded == 0)
            {
                star1.gameObject.SetActive(false);
                star2.gameObject.SetActive(false);
                star3.gameObject.SetActive(false);
            }
            btn.onClick.AddListener(LoadScene);
        }
        else
        {
            // show the locked button image
            btnImg.overrideSprite = lockedBtn;

            // hide the 3 stars
            star1.gameObject.SetActive(false);
            star2.gameObject.SetActive(false);
            star3.gameObject.SetActive(false);
        }


        #endregion

    }
   public void LoadScene()
    {
        LoadingCtrl.instance.ShowLoading();   
        SceneManager.LoadScene(sceneName);
    }

}
