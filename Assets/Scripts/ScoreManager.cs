using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TMPro.Examples {
	public class ScoreManager : MonoBehaviour {
		
		// Use this for initialization
		void Start () {
			//ScoreTextController.scoreText.text = "0";

			if (SceneManager.GetActiveScene ().buildIndex == 1) {
				ScoreController.score = 0;
			}
		}
		
		// Update is called once per frame
		void Update () {
			//ScoreTextController.scoreText.text = ScoreController.score.ToString ();
		}
	}
}
