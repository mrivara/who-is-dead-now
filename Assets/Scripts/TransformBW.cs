using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class TransformBW : MonoBehaviour

{
    public GameObject BG_01;
    public GameObject BG_BW_01;

    // Start is called before the first frame update
    // Update is called once per frame
    public void MundoBW()
    {
        BG_BW_01.SetActive(true);
    }
}
