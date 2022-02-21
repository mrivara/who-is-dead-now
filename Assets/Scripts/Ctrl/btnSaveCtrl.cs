using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnSaveCtrl : MonoBehaviour
{
    public GameData data;

    // Start is called before the first frame update
public void SaveData()
    {
        DataCtrl.instance.SaveData();
    }

    //public void LoadLVL()
    //{
    //    GameCtrl.instance.Inicio();
    //}
}
