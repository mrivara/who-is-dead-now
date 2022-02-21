using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShoot : MonoBehaviour {

    public GameObject cannonBall;
    // Use this for initialization
    public float firePower;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ShootCannon()
    {
        GameObject thisCannonBall = Instantiate(cannonBall, transform.position, transform.rotation);
        thisCannonBall.GetComponent<Rigidbody>().AddRelativeForce(0, 0, firePower, ForceMode.Impulse);

    }
}
