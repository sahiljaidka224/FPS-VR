using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour {

	// speed for the bullet
	public float speed = 50.0f;

	// for how long the bullet will remain in the scene
	public float lifeDuration = 5.0f;

	// to check if the bullet is shot by the player or the enemy
	private bool shotByPlayer;
	public bool ShotByPlayer { get { return shotByPlayer; } set {  shotByPlayer = value; } }

	private float lifeTimer;

	// Use this for initialization
	void Start () {
		lifeTimer = lifeDuration;
	}
	
	// Update is called once per frame
	void Update () {

		// transform bullets position
		transform.position += transform.forward * speed * Time.deltaTime;

		lifeTimer -= Time.deltaTime;
		if (lifeTimer <= 0f) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter (Collider other) {

		// if bullet collides with anything that has class <Walls>, Destroy the bullet.
		if (other.GetComponent<Walls> () != null) {
			Destroy (gameObject);
			return;
		}

		// if bullet collides with Play Button which is in the Start scene.
		if (other.CompareTag ("PlayButton")) {
			
			// Load the Game Scene.
			SceneManager.LoadScene (sceneName:"GameScene");

		} 
		// Check if Bullet collides with Credits Button.
		else if (other.CompareTag ("CreditsButton")) {
			
			if (other.GetComponent<InfoText> () != null) {

				// Get a 3D text which has a class Info Text : and display data in it.
				InfoText info = other.GetComponent<InfoText> ();
				info.hideDisplayInfoText ("SIT756" + System.Environment.NewLine + 
					"VR Developement Challenge" + System.Environment.NewLine + 
					"Submitted By : Sahil Jaidka" + System.Environment.NewLine + 
					"Student ID : 217528942"
				);

			}
		} 
		// Check if Bullet collides with Tutorial Button.
		else if (other.CompareTag ("TutorialButton")) {
			if (other.GetComponent<InfoText> () != null) {

				// Get a 3D text which has a class Info Text : and display data in it.
				InfoText info = other.GetComponent<InfoText> ();
				info.hideDisplayInfoText (
					"Hit Play button with Bullet to go into Game Scene." + System.Environment.NewLine + 
					"Player has to pick some objects with left trigger and" + System.Environment.NewLine +
					"take them to a particular area to win the game." + System.Environment.NewLine +
					"To move from one place to another left touchpad will be used." + System.Environment.NewLine +
					"Bullets can be collected by hitting crates with hammer."
				);

			}
		} 
		// Check if Bullet collides with Settings Button.
		else if (other.CompareTag ("SettingsButton")) {
			if (other.GetComponent<InfoText> () != null) {

				InfoText info = other.GetComponent<InfoText> ();
				info.hideDisplayInfoText ("");

			}
		} 
		// Check if Bullet collides with Exit Button.
		else if (other.CompareTag ("ExitButton")) {
			if (other.GetComponent<ResetExit> () != null) {

				// Call a function in ResetExit script to exit the scene back to Start Scene.
				ResetExit exit = other.GetComponent<ResetExit> ();
				exit.exitGame ();

			}
		} 
		// Check if Bullet collides with Reset Button.
		else if (other.CompareTag ("ResetButton")) {
			if (other.GetComponent<ResetExit> () != null) {

				// Call a function in ResetExit script to reset the scene.
				ResetExit reset = other.GetComponent<ResetExit> ();
				reset.resetGame ();

			}
		}

	}
}
