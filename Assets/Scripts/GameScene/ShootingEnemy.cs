using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : Enemy {

	public GameObject bulletPreFab;

	public float shootingInterval = 4f;
	public float shootingDistance = 10f;
	public float chasingDistance = 12f;
	public float chasingInterval = 2f;

	private Player player;
	private float shootingTimer;
	private float chasingTimer;
	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {

		// Find the player
		player = GameObject.Find ("[CameraRig]").GetComponent<Player> ();

		agent = GetComponent<NavMeshAgent> ();

		// to check not all the enemies shoot at the same time
		shootingTimer = Random.Range (0, shootingInterval);

	}
	
	// Update is called once per frame
	void Update () {

		// if player is null, nothing will happen, enemies will remain at the same place.
		if (player == null) {
			return;
		}
			
		shootingTimer -= Time.deltaTime;

		// check if shooting timer is not less than 0, and distance between player and enemy is less than or equals to shooting Distance
		if ((shootingTimer <= 0) && (Vector3.Distance (transform.position, player.transform.position) <= shootingDistance)) {
			shootingTimer = shootingInterval;

			// Instantiate a bullet, where the enemy is.
			GameObject bulletObject = Instantiate (bulletPreFab);

			// Bullet is shot by the enemy
			bulletObject.GetComponent<Bullet> ().ShotByPlayer = false;

			// give bullet a position
			bulletObject.transform.position = transform.position;
			bulletObject.transform.forward = (player.transform.position - transform.position).normalized;

		}

		chasingTimer -= Time.deltaTime;

		// check if chasing timer is not less than 0, and distance between player and enemy is less than or equals to chasing Distance
		if ((chasingTimer <= 0) && (Vector3.Distance (transform.position, player.transform.position) <= chasingDistance)) {
			chasingTimer = chasingInterval;

			// Tell the enemy to start following the player.
			agent.SetDestination (player.transform.position);
		}

	}
}
