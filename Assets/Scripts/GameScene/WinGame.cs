using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour {

	// to give feedback to the player about number of goals left.
	public GameObject gameWonText;

	// create a list to keep track of all the goal objects.
	private List <GameObject> goalObjects = new List<GameObject> ();

	// to check if goal objects are already collected.
	bool alreadyInside = false;

	// Use this for initialization
	void Start () {

		// empty the contents of the list.
		goalObjects = new List<GameObject> ();

		// Set a default text 
		gameWonText.GetComponent<TextMesh> ().text = "Number of Boxes left : 3";
	}

	void OnTriggerEnter (Collider other) {
		//print ("WinGame : OnTriggerEnter : working");

		// Iterate over the list to check if goal objects are already there
		for (int i = 0; i < goalObjects.Count; i++) {
			if ((goalObjects [i].name == other.gameObject.name)) {
				alreadyInside = true;
				//print ("WinGame : OnTriggerEnter : Already inside");	      
			} 
		}

		// if goal objects are not already inside the list check if the object colliding is the goal object or something else.
		if ((!alreadyInside) && ((other.gameObject.name == "KeyObjectOne") || (other.gameObject.name == "KeyObjectTwo") || (other.gameObject.name == "KeyObjectThree"))) {

			// add the object in the list.
			goalObjects.Add (other.gameObject);
		}

		int objectsLeft = 3 - goalObjects.Count;
		// Update the text on the basis of number of objects left.
		gameWonText.GetComponent<TextMesh> ().text = "Number of Boxes left : " + objectsLeft;
			

		// If Goal Objects Count == 3 , Display the Game Won Text and change the color of the Win Game Area.
		if (goalObjects.Count == 3) {
			gameWonText.GetComponent<TextMesh> ().text = "Game Won.";
			transform.GetComponent<Renderer> ().enabled = true;
			this.transform.GetComponent<Renderer> ().material.color = Color.red ;

			StartCoroutine (ChangeSceneIfGameWon ());

		}

	}

	IEnumerator ChangeSceneIfGameWon () {

		yield return new WaitForSeconds (5.0f);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

}
