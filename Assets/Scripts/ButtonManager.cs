using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace TMPro.Examples {
	public class ButtonManager : MonoBehaviour {

		public AudioMixer am;

		Resolution[] resolutionsArray;

		public TMP_Dropdown resolutionDropdown;

		public TextMeshProUGUI welcomeText;

		// Use this for initialization
		void Start(){
			if(SceneManager.GetActiveScene().buildIndex == 0){
				//Create an array with all possible resolutions
				resolutionsArray = Screen.resolutions;
				//Clear the resolution dropdown list
				resolutionDropdown.ClearOptions ();
				//Create a list to hold the resolutions
				List<string> options = new List<string>();
				//Add the possible resolutions from the array to the list
				for (int i = 0; i < resolutionsArray.Length; i++) {
					string option = resolutionsArray [i].width + "x" + resolutionsArray [i].height;
					options.Add (option);
				}
				//Add the resolution options to the dropdown list
				resolutionDropdown.AddOptions (options);

				welcomeText.text = Application.productName;
			}
		}

		/// <summary>
		/// Starts the game.
		/// </summary>
		public void StartButton(){
			SceneManager.LoadScene (1);
		}

		/// <summary>
		/// Exits the game.
		/// </summary>
		public void ExitButton(){
			Debug.Log ("Quit game.");
			Application.Quit ();
		}

		public void SetVolume(float volume){
			am.SetFloat ("masterVolume", volume);
		}

		public void SetQuality(int qualityIndex){
			QualitySettings.SetQualityLevel (qualityIndex);
		}

		public void SetFullScreen(bool isFullscreen){
			Screen.fullScreen = isFullscreen;
		}

		public void SetResolution(int resolutionIndex){
			Resolution resolution = resolutionsArray[resolutionIndex];
			Screen.SetResolution(resolution.width,resolution.height, Screen.fullScreen);
		}

		public void RetryButton(){
			SceneManager.LoadScene (1);
		}

		public void MenuButton(){
			SceneManager.LoadScene (0);
		}
	}
}
