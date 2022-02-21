using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class UI
{
    [Header("Text")]
    public Text txtCoinCount;
    public Text txtScore;
    public Text ammo;

    [Header("Images/ Sprites")]
    public Image heart1;
    public Image heart2;
    public Image heart3;
    public Image bossBattle;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Slider BossHealth;
    public Slider PlayerHealth;
    public Slider PlayerTransform;

    [Header("Popup Menus")]
    public GameObject panelGameOver;
    public GameObject LevelCompleteMenu;
    public GameObject panelMobileUI;
    public GameObject panelHUD;
    public GameObject panelPause;
    public GameObject panelSettings;
}
