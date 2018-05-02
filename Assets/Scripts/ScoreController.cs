using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreController{

	public static int playerLevel;
	public static int exp = 0;

	public static int score;

	public static void AddScore(int scoreGained){
		score += scoreGained;
	}

	public static void AddExp(int expGained){
		exp += expGained;
		if (exp > 100) {
			exp = exp - 100;
			playerLevel++;
		}
	}
}
