﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour {


	// Use this for initialization
	void Start () {
		this.GetComponent<Rigidbody> ().velocity = this.transform.forward * 10;
		Destroy (this.gameObject, 60.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider newCollision){
		if(newCollision.gameObject.tag == "Ground"){
			Destroy (this.gameObject);
		} else if(newCollision.gameObject.tag == "Player"){
			Destroy (this.gameObject);
			Debug.Log ("Lose");
		}
	}
}
