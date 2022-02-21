using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Punto de guardado, guarda las coordenadas del personaje 
/// </summary>
public class savePoint : MonoBehaviour
{
    public static savePoint instance;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("CPX", other.gameObject.transform.position.x);
            PlayerPrefs.SetFloat("CPY", other.gameObject.transform.position.y);
            Debug.Log(other.gameObject.transform.position.x);
            Debug.Log(other.gameObject.transform.position.y);
        }
    }

    public void DeleteSave()
    {
        //save = false;
        PlayerPrefs.DeleteKey("CPX");
        PlayerPrefs.DeleteKey("CPY");
    }
}
