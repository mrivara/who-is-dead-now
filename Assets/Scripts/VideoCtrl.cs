using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoCtrl : MonoBehaviour
{

    public Button Button1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ButtonDelay());
    }

    IEnumerator ButtonDelay()
    {
        Debug.Log(Time.time);
        yield return new WaitForSeconds(10f);
        Debug.Log(Time.time);

        // This line will be executed after 10 seconds passed
        Button1.gameObject.SetActive(true);
    }


}
