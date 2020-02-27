using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetExit : MonoBehaviour {

	public GameObject reset;
	public GameObject exit;

	public void resetGame () {

		// Reset the scene
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void exitGame () {

		// Exit back to previous scene
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
	}
}
