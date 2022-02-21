using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Networking;


public class DataCtrl : MonoBehaviour {

    public static DataCtrl instance = null;  
    public GameData data;  //agregado en clase 106
    public bool devMode;

    string dataFilePath;  //agregado en clase 106
    BinaryFormatter bf;   //agregado en clase 106


    void Start()
    {
        RefreshData();
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        bf = new BinaryFormatter(); //agregado en clase 106
        dataFilePath = Application.persistentDataPath + "/game.dat";
        Debug.Log(dataFilePath);
    }

    public void RefreshData()
    {
        if(File.Exists(dataFilePath))
        {
            FileStream fs = new FileStream(dataFilePath, FileMode.Open);
            data = (GameData)bf.Deserialize(fs);
            fs.Close();

            Debug.Log("Data Refreshed");
        }
    }

    public void SaveData()
    {       
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();

        Debug.Log("Data Save");     
    }

    public void SaveData(GameData data)
    {
        FileStream fs = new FileStream(dataFilePath, FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();

        Debug.Log("Data Save");
    }

    public bool isUnlocked(int levelNumber)
    {
        return data.levelData[levelNumber].isUnlocked;
    }

    public int getStars(int levelNumber)
    {
        return data.levelData[levelNumber].starsAwarded;
    }

    void OnEnable()
    {
        //RefreshData();
        CheckDB();
    }

    void CheckDB()
    {
        if (!File.Exists(dataFilePath))
        {
            #if UNITY_ANDROID

            CopyDB();

            #endif
        }
        //comentado 30112020
        //else
        //{
        //    if (SystemInfo.deviceType == DeviceType.Desktop)
        //    {
        //        string destFile = System.IO.Path.Combine(Application.streamingAssetsPath, "game.dat");
        //        File.Delete(destFile);
        //        File.Copy(dataFilePath, destFile);
        //    }

        //    if (devMode)
        //    {
        //        if (SystemInfo.deviceType == DeviceType.Handheld)
        //        {
        //            File.Delete(dataFilePath);
        //            CopyDB();
        //        }
        //    }

        //    RefreshData();
        //}
        //fin comentado 30112020
    }

    void CopyDB()
    {
        string srcFile = Path.Combine(Application.streamingAssetsPath, "game.dat");
        WWW downloader = new WWW(srcFile);
        while (!downloader.isDone)
        {
            // nothing to be done while downloader gets our db file
        }

        // then save to Application.persistentDataPath
        File.WriteAllBytes(dataFilePath, downloader.bytes);
        RefreshData();
    }

}
