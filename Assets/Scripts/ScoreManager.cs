using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TMPro.Examples {
	public class ScoreManager : MonoBehaviour {

		//Get the exp bar
		public SimpleHealthBar expBar;

		//Get the text which displays the player level
		public TextMeshProUGUI levelText;
		//Get the text which displays the score
		public TextMeshProUGUI scoreText;
		
		// Use this for initialization
		void Start () {
			//If the loaded scene is level 1, set everything to 0
			if (SceneManager.GetActiveScene ().buildIndex == 1) {
				ScoreController.score = 0;
				scoreText.text = "0";
				levelText.text = "0";
				ScoreController.playerLevel = 0;
				ScoreController.exp = 0;
			}
		}
		
		// Update is called once per frame
		void Update () {
			//Update the score, exp and player level
			scoreText.text = ScoreController.score.ToString ();
			expBar.UpdateBar (ScoreController.exp,100);
			levelText.text = ScoreController.playerLevel.ToString ();
		}
	}
}
