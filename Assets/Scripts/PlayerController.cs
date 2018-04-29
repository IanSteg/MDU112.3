﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	/// <summary>
	/// Get the players rigidbody and SpriteRenderer.
	/// </summary>
	Rigidbody rb;
	SpriteRenderer sr;

	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	/// <summary>
	/// Define the ints.
	/// </summary>
	//The speed the player moves.
	public int speed;
	//The maximum speed the player can move
	public int maxSpeed;
	//The jump height of the player.
	public int jumpHeight;

	int health = 100;

	public int knockBackForce = 10;

	/// <summary>
	/// Define the bools.
	/// </summary>
	//Can the player jump?
	bool canJump = false;

	public SimpleHealthBar healthBar;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		sr = GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (!PauseMenu.gameIsPaused) {
			//Move Right
			if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
				sr.flipX = false;
				bulletSpawn.localPosition = new Vector3 (0.25f, 0.0f, 0.0f);
				bulletSpawn.rotation = Quaternion.Euler(new Vector3 (0.0f, 90.0f, 0.0f));
				if (rb.velocity.x < maxSpeed) {
					rb.AddForce (Vector3.right * speed);
				} else {
					rb.velocity = new Vector3 (maxSpeed, rb.velocity.y, rb.velocity.z);
				}
			} else
			//Move Left
			if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
					sr.flipX = true;
					bulletSpawn.localPosition = new Vector3 (-0.25f, 0.0f, 0.0f);
					bulletSpawn.rotation = Quaternion.Euler(new Vector3 (0.0f, -90.0f, 0.0f));
					if (-rb.velocity.x < -maxSpeed || rb.velocity.x > -maxSpeed) {
						rb.AddForce (Vector3.left * speed);
					} else {
						rb.velocity = new Vector3 (-maxSpeed, rb.velocity.y, rb.velocity.z);
					}
				}
				//Jump
				if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
					if (canJump) {
						rb.velocity = new Vector3 (rb.velocity.x, jumpHeight, rb.velocity.z);
						canJump = false;
					}
				}
				//Shoot
				if (Input.GetKeyDown(KeyCode.Space)) {
					Shoot ();
				}
			}
		}

	/// <summary>
	/// Make the player shoot.
	/// </summary>
	void Shoot(){
		Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="newCollision">New collision.</param>
	void OnCollisionEnter(Collision newCollision){
		if (newCollision.gameObject.tag == "Enemy") {
			takeDamage(Random.Range(10,25),ref health,newCollision);
		}
		//When the player is on the ground, allow them to jump
		if (newCollision.gameObject.tag == "Ground") {
			canJump = true;
		}
	}

	int takeDamage (int damage,ref int currentHealth, Collision col){
		Vector3 pushDirection;
		currentHealth -= damage;
		healthBar.UpdateBar (currentHealth, 100);
		pushDirection = col.impulse * -10;
		rb.AddForce (pushDirection * knockBackForce);
		Debug.Log (currentHealth);
		if (currentHealth <= 0) {
			Dead ();
		}
		return currentHealth;
	}

	void Dead(){
		SceneManager.LoadScene ("Dead");
	}
}