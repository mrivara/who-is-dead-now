using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppPause : MonoBehaviour {
    public UI ui;

    public void Pause()
    {
        ui.panelGameOver.SetActive(true);
    }
}
