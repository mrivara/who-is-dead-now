using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


/// <summary>
/// Data Model for your game data
/// </summary>

[Serializable]
public class GameData

{
    public int coinCount;
    public int score;
    public int lives;
    public int ammo;
    public int kills;
    public bool BossActive;
    public bool isFirstBoot; //setea si es la primera vez que inicia el juego
    public bool lvlControl;
    public LevelData[] levelData;

    public bool playSound;
    public bool playMusic;
}
