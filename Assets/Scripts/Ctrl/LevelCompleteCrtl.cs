using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class LevelCompleteCrtl : MonoBehaviour
{

    // Use this for initialization
    public Button btnNext;
    public Sprite goldenStar;
    public Image lvlComple;
    public Image lvlNoComple;
    public Image Start1;
    public Image Start2;
    public Image Start3;
    public Image lvlImg;
    public Image lvlTryAgain;
    //public Sprite lvlImgUp;
    public Text txtScore;
    public int levelNumber;

    //[HideInInspector]
    public int score;
    public int ScoreForThreeStars;
    public int ScoreForTwoStars;
    public int ScoreForOneStars;
    public int ScoreForNextLevel;
    public float animStartDelay;
    public float animDelay;
    public float animDelay2;
    public float animLvl;

    bool showTwoStars, showThreeStars;

    void Start()
    {
        //comentado para testing 
        score = GameCtrl.instance.GetScore();
        txtScore.text = "" + score;
        lvlComple.enabled = false;
        lvlNoComple.enabled = false;
        lvlImg.enabled = false;
        lvlTryAgain.enabled = false;

        if (score >= ScoreForThreeStars)
        {
            GameCtrl.instance.UnlockLevel(levelNumber);
            showThreeStars = true;
            GameCtrl.instance.SetStarsAwarded(levelNumber, 3);
            Invoke("ShowGoldenStars", animStartDelay);
            //Invoke("ShowLvlAnim", animLvl);
            lvlComple.enabled = true;
            lvlImg.enabled = true;
        }

        if (score >= ScoreForTwoStars && score < ScoreForThreeStars)
        {
            GameCtrl.instance.UnlockLevel(levelNumber);
            showTwoStars = true;
            GameCtrl.instance.SetStarsAwarded(levelNumber, 2);
            Invoke("ShowGoldenStars", animStartDelay);
            lvlComple.enabled = true;
            lvlImg.enabled = true;
        }

        if (score >= ScoreForOneStars && score != 0 && score < ScoreForTwoStars)
        {
            GameCtrl.instance.UnlockLevel(levelNumber);
            GameCtrl.instance.SetStarsAwarded(levelNumber, 1);
            Invoke("ShowGoldenStars", animStartDelay);
            lvlComple.enabled = true;
            lvlImg.enabled = true;
        }

        if (score >= ScoreForNextLevel && score != 0 && score < ScoreForOneStars)
        {
            GameCtrl.instance.UnlockLevel(levelNumber);
            //GameCtrl.instance.SetStarsAwarded(levelNumber, 0);
            //Invoke("ShowGoldenStars", animStartDelay);
            Invoke("CheckLevelStatus", 1.2f);
            lvlComple.enabled = true;
            //lvlImg.enabled = true;
        }

        if (score < ScoreForNextLevel)
        {
            lvlNoComple.enabled = true;
            lvlTryAgain.enabled = true;
        }

    }

    void ShowGoldenStars()
    {
        StartCoroutine("HandleFirstStarAnim", Start1);
    }

    void ShowLvlAnim()
    {
        StartCoroutine("lvlStarAnim", lvlImg);
    }

    IEnumerator HandleFirstStarAnim(Image starImg)
    {
        DoAnim(starImg);
        yield return new WaitForSeconds(animDelay);
        //called if more than one star is awarded
        if (showTwoStars || showThreeStars)
        {
            StartCoroutine("HandleSecondStarAnim", Start2);
        }
        else
            Invoke("CheckLevelStatus", 1.2f);
    }

    IEnumerator HandleSecondStarAnim(Image starImg)
    {
        DoAnim(starImg);
        yield return new WaitForSeconds(animDelay);

        showTwoStars = false;
        if (showThreeStars)
            StartCoroutine("HandleThirdStarAnim", Start3);
        else
            Invoke("CheckLevelStatus", 1.2f);

    }

    IEnumerator HandleThirdStarAnim(Image starImg)
    {
        DoAnim(starImg);
        yield return new WaitForSeconds(animDelay);

        showThreeStars = false;
        Invoke("CheckLevelStatus", 1.2f);
    }

    //IEnumerator lvlStarAnim(Image lvlImg)
    //{
    //    DoAnim2(lvlImg);
    //    yield return new WaitForSeconds(animDelay2);
    //}

    void CheckLevelStatus()
    {
        if (score >= ScoreForNextLevel)
        {
            btnNext.interactable = true;
            SFXCtrl.instance.ShowBulletSparkle(btnNext.gameObject.transform.position);
            //GameCtrl.instance.UnlockLevel(levelNumber);
        }
        else
        {
            btnNext.interactable = false;
        }
    }

    void DoAnim(Image starImg)
    {
        //aumenta tamaño de estrella
        starImg.rectTransform.sizeDelta = new Vector2(150f, 150f);
        //muestra estrella dorada
        starImg.sprite = goldenStar;
        //vuelve la estrella al tamaño por defecto usando el animador DOTween
        RectTransform t = starImg.rectTransform;
        t.DOSizeDelta(new Vector2(100f, 100f), 0.5f, false);
        //audio
        //Ver porque no anda
        //efectos visuales
        SFXCtrl.instance.ShowBulletSparkle(starImg.gameObject.transform.position);
    }

    //void DoAnim2(Image lvlImg)
    //{
    //    //aumenta tamaño de estrella
    //    lvlImg.rectTransform.sizeDelta = new Vector2(571f, 831f);
    //    //muestra estrella dorada
    //    lvlImg.sprite = lvlImgUp;
    //    //vuelve la estrella al tamaño por defecto usando el animador DOTween
    //    RectTransform t = lvlImg.rectTransform;
    //    t.DOSizeDelta(new Vector2(381f, 554f), 0.5f, false);
    //    //audio
    //    //Ver porque no anda
    //    //efectos visuales
    //    SFXCtrl.instance.ShowBulletSparkle(lvlImg.gameObject.transform.position);
    //}
}