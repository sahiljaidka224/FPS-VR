// Reference from : 2017 T2 Class notes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public GameObject bulletPreFab;
	public GameObject gun;

	public int initialAmmo = 12;

	public SteamVR_TrackedObject rightController; 

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		var device = SteamVR_Controller.Input ((int)rightController.index);
		if (device.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
			
			GameObject bulletObject = Instantiate (bulletPreFab);
			bulletObject.GetComponent<Bullet> ().ShotByPlayer = true;
			bulletObject.transform.position = gun.transform.position + gun.transform.forward;
			bulletObject.transform.rotation = gun.transform.rotation;

		}
	}
}
