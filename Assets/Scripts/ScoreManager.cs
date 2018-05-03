using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TMPro.Examples {
	public class ScoreManager : MonoBehaviour {

		public SimpleHealthBar expBar;

		public TextMeshProUGUI levelText;
		public TextMeshProUGUI scoreText;
		
		// Use this for initialization
		void Start () {
			if (SceneManager.GetActiveScene ().buildIndex == 1) {
				ScoreController.score = 0;
				scoreText.text = "0";
				levelText.text = "0";
				ScoreController.playerLevel = 0;
			}
		}
		
		// Update is called once per frame
		void Update () {
			scoreText.text = ScoreController.score.ToString ();
			expBar.UpdateBar (ScoreController.exp,100);
			levelText.text = ScoreController.playerLevel.ToString ();
		}
	}
}
