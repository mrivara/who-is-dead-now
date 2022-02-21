using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvlComplete : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("ShowLevelCompleteMenu", 3f);
        }
    }

    void ShowLevelCompleteMenu()
    {
        GameCtrl.instance.ShowLevelCompleteMenu();
    }

}
