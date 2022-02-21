using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsCtrl : MonoBehaviour
{
    public string facebookURL, igURL, pageURL, ratingURL;

    public void FacebookLike()
    {
        Application.OpenURL(facebookURL);
    }

    public void IgLike()
    {
        Application.OpenURL(igURL);
    }

    public void info()
    {
        SceneManager.LoadScene("Credits");
    }
}
