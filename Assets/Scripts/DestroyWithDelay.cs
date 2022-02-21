using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Destruir el objeto
/// </summary>
public class DestroyWithDelay : MonoBehaviour {

    public float delay;

	void Start () {
        Destroy(gameObject, delay);

	}
}
