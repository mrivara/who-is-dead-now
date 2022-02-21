using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using DG.Tweening;


public class menuctrl : MonoBehaviour 
{
    public static menuctrl instance;

    [HideInInspector]
    //public GameData data;
    public GameData data;
    public UI ui;

    public void LoadScene(string sceneName)
    {
        if (data.isFirstBoot)
        { SceneManager.LoadScene("Tuto"); }
        else
        { SceneManager.LoadScene(sceneName); }            
    }

    public void LoadScene2()
    {
        SceneManager.LoadScene("MapsMenu");
    }

    public void ShowSettingsPanel()
    {
        ui.panelSettings.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 0.7f, false);     
    }
    public void HideSettingsPanel()
    {
        ui.panelSettings.gameObject.GetComponent<RectTransform>().DOAnchorPosY(522f, 0.7f, false);
    }
}
