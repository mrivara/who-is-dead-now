using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {

    public GameData data;

    public void LoadScene(string sceneName)
    {
        GameCtrl.instance.Inicio();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadCurrent()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void LoadScene2(string sceneName)
    {      
            SceneManager.LoadScene(sceneName);     
    }

    public void RestartLVL()
    {
        GameCtrl.instance.updateLVL();
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    public void LoadMenuLVL(string sceneName)
    {
        GameCtrl.instance.updateLVL();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene3()
    {

        Debug.Log("es la primera vez: " + data.isFirstBoot);
        //       SceneManager.LoadScene("Tuto");
        SceneManager.LoadScene("MapsMenu");
    }
}
