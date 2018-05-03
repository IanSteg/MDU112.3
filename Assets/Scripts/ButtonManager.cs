using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace TMPro.Examples {
	public class ButtonManager : MonoBehaviour {

		//Get the audio mixer
		public AudioMixer am;

		//Get an array of available resolutions
		Resolution[] resolutionsArray;

		//Get the dropdown for the list of resolutions
		public TMP_Dropdown resolutionDropdown;

		//Get the title screen text
		public TextMeshProUGUI welcomeText;
		//Get the text that displays the score when the player dies or wins
		public TextMeshProUGUI scoreText;

		// Use this for initialization
		void Start(){
			//If the current scene is the main menu
			if (SceneManager.GetActiveScene ().buildIndex == 0) {
				//Create an array with all possible resolutions
				resolutionsArray = Screen.resolutions;
				//Clear the resolution dropdown list
				resolutionDropdown.ClearOptions ();
				//Create a list to hold the resolutions
				List<string> options = new List<string> ();
				//Add the possible resolutions from the array to the list
				for (int i = 0; i < resolutionsArray.Length; i++) {
					string option = resolutionsArray [i].width + "x" + resolutionsArray [i].height;
					options.Add (option);
				}
				//Add the resolution options to the dropdown list
				resolutionDropdown.AddOptions (options);
				//Set the welcome text to the name of the application
				welcomeText.text = Application.productName;
			//If the current scene is the play win scene or death scene
			} else if (SceneManager.GetActiveScene ().name == "Win") {
				//Display the players score.
				scoreText.text = ScoreController.score.ToString ();
			}
		}

		/// <summary>
		/// Starts the game.
		/// </summary>
		public void StartButton(){
			//When the start button is pressed, load level 1
			SceneManager.LoadScene (1);
		}

		/// <summary>
		/// Exits the game.
		/// </summary>
		public void ExitButton(){
			//If the exit button is pressed, quit the game
			Debug.Log ("Quit game.");
			Application.Quit ();
		}

		public void SetVolume(float volume){
			//Set the volume based on the volume slider
			am.SetFloat ("masterVolume", volume);
		}

		public void SetQuality(int qualityIndex){
			//Set the quality level based on the quality dropdown
			QualitySettings.SetQualityLevel (qualityIndex);
		}

		public void SetFullScreen(bool isFullscreen){
			//Toggle fullscreen based on the fullscreen checkbox
			Screen.fullScreen = isFullscreen;
			Debug.Log ("Fullscreen toggled");
		}

		public void SetResolution(int resolutionIndex){
			//Set the resolution based on the resolution dropdown
			Resolution resolution = resolutionsArray[resolutionIndex];
			Screen.SetResolution(resolution.width,resolution.height, Screen.fullScreen);
		}

		public void RetryButton(){
			//Load level 1 when the retry button it pressed
			SceneManager.LoadScene ("Level1");
		}

		public void MenuButton(){
			//Go back to the main menu
			SceneManager.LoadScene ("Main Menu");
		}
	}
}
