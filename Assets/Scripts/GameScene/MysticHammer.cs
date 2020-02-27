using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysticHammer : MonoBehaviour {

	public GameObject cameraRig;

	// To check the objects Hammer collides with. 
	void OnTriggerEnter (Collider other) {

		Player player = cameraRig.GetComponent<Player> ();

		switch (other.gameObject.name) {

		case "AmmoCrate":

			// if hammer collides with AmmoCrate, increase ammo for the player and destroy the Ammo Crate.
			player.initialAmmo += 100;
			player.updateAmmo ();
			Destroy (other.gameObject);
			break;

		case "HealthCrate":

			// if hammer collides with HealthCrate, increase health for the player and destroy the Health Crate.
			player.initialHealth = 100;
			player.updateHealth ();
			Destroy (other.gameObject);
			break;

		default:
			break;
		}

	}
}
