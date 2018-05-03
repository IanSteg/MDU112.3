using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	/// <summary>
	/// Get the enemies rigidbody
	/// </summary>
	Rigidbody rb;

	//set the enemy to move left
	bool movingLeft = true;

	/// <summary>
	/// Define the ints.
	/// </summary>
	//The maximum speed the enemy can move
	public int speed;

	// Use this for initialization
	void Start () {
		//Get the enemies rigidbody
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Make the enemy move
		Move ();
	}

	/// <summary>
	/// Raises the collision enter event.
	/// </summary>
	/// <param name="newCollision">New collision.</param>
	void OnTriggerEnter(Collider newCollider){
		//If the enemy hits a wall, toggle the direction of travel
		if (newCollider.gameObject.tag == "Ground" || newCollider.gameObject.tag == "InteractableWall") {
			movingLeft = !movingLeft;
		}
	}

	/// <summary>
	/// Move this instance.
	/// </summary>
	void Move(){
		//Toggle the sprites direction of travel
		if (movingLeft) {
			//Flip the enemy to face left
			this.transform.localScale = new Vector3(2,2,2);
			rb.velocity = new Vector3 (-speed, rb.velocity.y, rb.velocity.z);
		} else {
			//Flip the enemy to face right
			this.transform.localScale = new Vector3(-2,2,2);
			rb.velocity = new Vector3 (speed, rb.velocity.y, rb.velocity.z); 	
		}
	}
}
