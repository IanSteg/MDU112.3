using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreController{
	public static int score;

	public static void AddScore(int scoreGained){
		score += scoreGained;
		Debug.Log (score);
	}
}
