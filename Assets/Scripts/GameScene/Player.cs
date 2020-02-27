 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	// create a prefab object for the bullet.
	public GameObject bulletPreFab;

	// create a game object for the gun.
	public GameObject gun;

	// create a prefab for the enemy.
	public GameObject enemyPrefab;

	// give some initial ammo to the player.
	public int initialAmmo = 200;

	// give some intial health to the player.
	public int initialHealth = 50;

	// for the player player can't get hurt again.
	public float hurtDuration = 1.0f;

	// generate number of enemies.
	public int numberOfEnemies = 15;

	// display text
	public GameObject gameOverWonAmmoHealth;

	private int health;
	private int ammo;

	// check if player is hurt.
	private bool isHurt;

	// Use this for initialization
	void Start () {

		// Initialise ammo and health.
		health = initialHealth;
		ammo = initialAmmo;

		// Update game over text by calling this function
		gameOverTextUpdate (false, "");

		// Call generate enemies function.
		generateEnemies ();
	}
	
	// Update is called once per frame
	void Update () {

		// If Right trigger is pressed and ammo count is greater than 0.
		if ((Input.GetAxis("RightTrigger") > 0.5f) && (ammo > 0)) {

			// Decrease number of ammo player has.
			ammo--;

			// Instantiate bullet prefab 
			GameObject bulletObject = Instantiate (bulletPreFab);

			// To keep a track who is shooting the bullets, player or the enemy
			bulletObject.GetComponent<Bullet> ().ShotByPlayer = true;

			// Instantiate bullet at a point
			bulletObject.transform.position = gun.transform.position + gun.transform.forward;
			bulletObject.transform.forward = gun.transform.forward;

		}

		// if health decrease below or equals to zero, Game is over player needs to start again.
		if (health < 40) {
			gameOverTextUpdate (true, "Health : " + health);
		} else if (health <= 0) {
			gameOverTextUpdate (true, "GAME OVER");
			StartCoroutine (ChangeSceneIfGameOver ());
		} else {
			gameOverTextUpdate (false, "");
		}

	}

	void generateEnemies () {

		// create enemies at a random position 
		for (int i = 0; i < numberOfEnemies; i++) {

			Vector3 theNewPos = new Vector3 (Random.Range (-30 , 30), 0.9f, Random.Range (-20 , 20) );
			Instantiate(enemyPrefab,theNewPos,transform.rotation);

		}

	}

	void OnTriggerEnter (Collider other) {

		// if player is already hurt, bullets won't affect him
		if (!isHurt) {

			// check if player collides with enemy
			if (other.GetComponent<Enemy> () != null) {

				// Decrease the health of the player as described in the enemy class.
				Enemy enemy = other.GetComponent<Enemy> ();
				health -= enemy.damage;

				// change value of isHurt = true.
				isHurt = true;

			}
			// check if player collides with bullet
			else if (other.GetComponent<Bullet> () != null) {

				Bullet bullet = other.GetComponent<Bullet> ();
				// check if bullet is shot by the player.
				if (!bullet.ShotByPlayer) {
					isHurt = true;

					health -= 5;
				}
			}

			print ("Health : " + health + " Ammo : " + ammo);
			StartCoroutine (HurtRoutine ());
		} 

		/*
		if (other.GetComponent<AmmoCrate> () != null) {

			AmmoCrate ammoCrate = other.GetComponent<AmmoCrate> ();
			ammo += ammoCrate.ammo;

		} else if (other.GetComponent<HealthCrate> () != null) {

			HealthCrate healthCrate = other.GetComponent<HealthCrate> ();
			health += healthCrate.health;

			if (health > 100) {
				health = 100;
			}
		} 
		*/

	}

	void gameOverTextUpdate (bool show, string text) {

		// hide the game over 3d text

		gameOverWonAmmoHealth.SetActive (show);
		gameOverWonAmmoHealth.GetComponent<TextMesh> ().text = text;

	}

	IEnumerator ChangeSceneIfGameOver () {

		yield return new WaitForSeconds (5.0f);
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
	}

	IEnumerator HurtRoutine () {
		yield return new WaitForSeconds (hurtDuration);
		isHurt = false;
	}
		
	public void updateAmmo () {
		ammo = initialAmmo;
	}

	public void updateHealth () {
		health = initialHealth;
	}
}