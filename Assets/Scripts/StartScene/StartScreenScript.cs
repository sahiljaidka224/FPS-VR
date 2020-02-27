// Reference from : 2017 T2 Class notes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenScript : MonoBehaviour {

	public GameObject menuButtonContainersObject;

	public GameObject controllerObject;

	public Transform cameraRigTranform;
	public Transform headTransform;

	public GameObject laserPrefab;

	private GameObject laser;
	private Transform laserTransform;

	private Vector3 hitPoint;
	private bool shouldTeleport;
	private SteamVR_TrackedObject trackedObj;

	private GameObject hitGameObject;

	SteamVR_Controller.Device Controller () {
		return SteamVR_Controller.Input ((int)trackedObj.index);
	}

	void Awake () {
		trackedObj = controllerObject.GetComponent <SteamVR_TrackedObject>();
	}

	// Use this for initialization
	void Start () {
		
		shouldTeleport = false;
		laser = Instantiate (laserPrefab);
		laserTransform = laser.transform;
	}
	
	// Update is called once per frame
	void Update () {

		//Is the touch pad held down?

		if (Controller ().GetPress (SteamVR_Controller.ButtonMask.Touchpad)) {
			RaycastHit hit;

			if (Physics.Raycast (trackedObj.transform.position, transform.forward, out hit, 100.0f)) {
				hitPoint = hit.point;
				ShowLaser (hit);
				shouldTeleport = true;
				hitGameObject = hit.transform.gameObject;
			}
		} else {
			//Touchpad not held down, hide laser & teleport reticle
			laser.SetActive (false);
		}

		// Touchpad released this frame & valid teleport position found.
		if (Controller ().GetPressUp (SteamVR_Controller.ButtonMask.Touchpad) && shouldTeleport) {

			print (hitGameObject.name);
			if (hitGameObject != null) {

				if (hitGameObject.GetComponent<Walls> () == null) {

					TeleportStart ();
					hitGameObject = null;
				}

			}

		}

	}

	void ShowLaser (RaycastHit hit) {

		laser.SetActive (true); // Show the laser.
		laserTransform.position = Vector3.Lerp(trackedObj.transform.position, hitPoint, 0.5f);
		laserTransform.LookAt (hitPoint);
		laserTransform.localScale = new Vector3 (laserTransform.localScale.x, laserTransform.localScale.y, hit.distance);
	}

	void TeleportStart () {
		shouldTeleport = false;
		Vector3 difference = cameraRigTranform.position - headTransform.position;
		difference.y = 0;
		cameraRigTranform.position = hitPoint + difference;
	}


}
