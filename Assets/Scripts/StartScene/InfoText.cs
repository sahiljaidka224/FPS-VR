using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoText : MonoBehaviour {

	public GameObject infoText;
	public GameObject lookAroundText;
	public GameObject settingsObject;

	// Use this for initialization
	void Start () {
		infoText.SetActive (false);
		lookAroundText.SetActive (false);
		settingsObject.SetActive (false);
	}

	public void hideDisplayInfoText (string textToBeDisplayed) {

		lookAroundText.SetActive (true);
		//print ("InfoText : hideDisplayInfoText : textToBeDisplayed " + textToBeDisplayed);
		if (textToBeDisplayed.Equals ("")) {
			settingsObject.SetActive (true);
		} else {
			infoText.GetComponent<TextMesh> ().text = textToBeDisplayed;
			infoText.SetActive (true);
		}

	}
}
