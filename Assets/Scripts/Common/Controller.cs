// Reference from : https://www.youtube.com/watch?v=GlznaqyF-gI&t=4s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	public SteamVR_Controller.Device controller {
		get {
			return SteamVR_Controller.Input ((int)GetComponent<SteamVR_TrackedObject>().index);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
