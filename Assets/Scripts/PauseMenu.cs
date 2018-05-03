using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	//Is the game paused?
	public static bool gameIsPaused = false;

	//Get the pause menu
	public GameObject pauseMenuUI;

	// Use this for initialization
	void Start(){
		//Set the game to not paused on start
		Resume ();
	}

	// Update is called once per frame
	void Update () {
		//If the esc key is pressed, toggle pause
		if(Input.GetKeyDown(KeyCode.Escape)){
			if (gameIsPaused) {
				Resume ();
			} else {
				Pause ();
			}
		}
	}

	public void Resume(){
		//Disable the pause ui
		pauseMenuUI.SetActive (false);
		//Reset the time scale
		Time.timeScale = 1f;
		//set the paused bool to false
		gameIsPaused = false;
	}

	void Pause(){
		//Enable the pause ui
		pauseMenuUI.SetActive (true);
		//Stop the game
		Time.timeScale = 0f;
		//Set the game to paused
		gameIsPaused = true;
	}

	public void LoadMenu(){
		//Load the main menu scene and reset the time scale
		Time.timeScale = 1f;
		SceneManager.LoadScene (0);
	}

	public void QuitGame(){
		//Quit the game
		Application.Quit ();
	}
}
