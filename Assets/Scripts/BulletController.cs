using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Set the velocity of the bullet to 10
		this.GetComponent<Rigidbody> ().velocity = this.transform.forward * 10;
		//Destroy the bullet after 60 seconds
		Destroy (this.gameObject, 60.0f);
	}

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="newCollision">New collision.</param>
	void OnTriggerEnter(Collider newCollision){
		//If the bullet collides with a wall, destroy it
		if(newCollision.gameObject.tag == "Ground" || newCollision.gameObject.tag == "InteractableWall"){
			Destroy (this.gameObject);
		//If the bullet collides with an enemy, add 20 exp, 10 score and destroy both the bullet and enemy
		} else if(newCollision.gameObject.tag == "Enemy"){
			ScoreController.AddExp (20);
			ScoreController.AddScore (10);
			Destroy (newCollision.gameObject);
			Destroy (this.gameObject);
		}
	}
}
