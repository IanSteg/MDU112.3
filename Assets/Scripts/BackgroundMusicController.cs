using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour {

	public AudioSource audioSource;
	public AudioClip backgroundMusic1;
	public AudioClip backgroundMusic2;

	// Use this for initialization
	void Start () {
		StartCoroutine (backgroundMusic ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator backgroundMusic(){
		audioSource.clip = backgroundMusic1;
		yield return new WaitForSeconds (backgroundMusic1.length);
		audioSource.clip = backgroundMusic2;
		yield return new WaitForSeconds (backgroundMusic2.length);
		StartCoroutine (backgroundMusic ());
	}
}
