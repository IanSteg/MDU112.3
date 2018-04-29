using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro.Examples {
	public class ScoreManager : MonoBehaviour {

	
		public TextMeshProUGUI scoreText;

		// Use this for initialization
		void Start () {
			scoreText.text = "0";
		}
		
		// Update is called once per frame
		void Update () {
			scoreText.text = ScoreController.score.ToString ();
		}
	}
}
