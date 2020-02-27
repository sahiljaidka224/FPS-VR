using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health = 5;
	public int damage = 5;

	void OnTriggerEnter (Collider other) {

		// Check if the Enemy Collides with the bullet

		if (other.GetComponent<Bullet> () != null) {

			Bullet bullet = other.GetComponent<Bullet> ();

			// Check if the Bullet is shot by the player and not by any other enemy.
			if (bullet.ShotByPlayer) {

				// If condition is true, Destroy the enemy.
				Destroy (gameObject); 
			}
		}
	}

}
