using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	/// <summary>
	/// Get the enemies rigidbody and SpriteRenderer.
	/// </summary>
	Rigidbody rb;
	SpriteRenderer sr;

	bool movingLeft = true;

	/// <summary>
	/// Define the ints.
	/// </summary>
	//The maximum speed the enemy can move
	public int speed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="newCollision">New collision.</param>
	void OnCollisionEnter(Collision newCollision){
		if (newCollision.gameObject.tag == "Ground") {
			movingLeft = !movingLeft;
		}
	}

	/// <summary>
	/// Move this instance.
	/// </summary>
	void Move(){
		if (movingLeft) {
			sr.flipX = false;
			rb.velocity = new Vector3 (-speed, rb.velocity.y, rb.velocity.z);
		} else {
			sr.flipX = true;
			rb.velocity = new Vector3 (speed, rb.velocity.y, rb.velocity.z); 	
		}
	}
}
