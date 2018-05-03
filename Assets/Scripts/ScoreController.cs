using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreController{

	//The player level
	public static int playerLevel;
	//The player's xp
	public static int exp = 0;
	//The player's score
	public static int score;
	//Is the luck boost enabled
	public static bool luckBoost = false;

	public static void AddScore(int scoreGained){
		//If the luck boost is enabled, double the amount of score gained
		if (luckBoost) {
			scoreGained = scoreGained * 2;
			score += scoreGained;
		//Add the amount of score gained to the player's score
		} else {
			score += scoreGained;
		}
	}

	public static void AddExp(int expGained){
		//Add xp
		//If the player has 100 xp, add 1 to their level and reset their xp
		exp += expGained;
		if (exp >= 100) {
			exp = exp - 100;
			playerLevel++;
		}
	}
}
