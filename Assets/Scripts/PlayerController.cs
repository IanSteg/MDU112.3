using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TMPro.Examples {
	public class PlayerController : MonoBehaviour {
		/// <summary>
		/// Get the player's rigidbody
		/// </summary>
		Rigidbody rb;

		//Get the bullet prefab and where to spawn them
		public GameObject bulletPrefab;
		public Transform bulletSpawn;

		/// <summary>
		/// Define the ints.
		/// </summary>
		//The speed the player moves.
		public int speed;
		//The maximum speed the player can move
		public int maxSpeed;
		//The jump height of the player.
		public int jumpHeight;
		//How much health the player has
		int health = 100;
		
		//public int knockBackForce = 10;
		//How much to the player has to complete the level
		int time = 180;
		//How many enemies are remaining
		int enemiesRemaining;

		/// <summary>
		/// Define the bools.
		/// </summary>
		//Can the player jump?
		bool canJump = false;
		//Is the agility boost active?
		bool agilityBoost = false;
		//The health bar
		public SimpleHealthBar healthBar;
		//The timer text
		public TextMeshProUGUI timerText;
		//The text that shows the number of enemies remaining
		public TextMeshProUGUI enemiesRemainingText;
		//Text that shows if the agility boost is enabled
		public TextMeshProUGUI agilityText;
		//Text that shows if the lucj boost is enabled
		public TextMeshProUGUI luckText;

		public AudioSource audioSource;
		public AudioClip gunShot;

		// Use this for initialization
		void Start () {
			//Get the player's rigidbody
			rb = GetComponent<Rigidbody> ();
			//Start the timer
			StartCoroutine(Timer(1));
		}
		
		// Update is called once per frame
		void Update () {
			//If the game is not paused, allow movement
			if (!PauseMenu.gameIsPaused) {
				//Move Right if the D or right arrow key is down
				if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
					//Flip the player to face right
					this.transform.localScale = new Vector3(2,2,2);
					//If the player is moving faster than the maximum speed, set the speed to the maximum speed
					if (rb.velocity.x < maxSpeed) {
						rb.AddForce (Vector3.right * speed);
					} else {
						rb.velocity = new Vector3 (maxSpeed, rb.velocity.y, rb.velocity.z);
					}
				} else
				//Move Left if the A or left arrow keys are down
				if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
						//Flip the player to face left
						this.transform.localScale = new Vector3(-2,2,2);
						//If the player is moving faster than the maximum speed, set the speed to the maximum speed
						if (-rb.velocity.x < -maxSpeed || rb.velocity.x > -maxSpeed) {
							rb.AddForce (Vector3.left * speed);
						} else {
							rb.velocity = new Vector3 (-maxSpeed, rb.velocity.y, rb.velocity.z);
						}
					}
					//Jump if the W or up arrow keys are down
					if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
						if (canJump) {
							rb.velocity = new Vector3 (rb.velocity.x, jumpHeight, rb.velocity.z);
							canJump = false;
						}
					}
					//Shoot of the space key is pressed
					if (Input.GetKeyDown(KeyCode.Space)) {
						Shoot ();
					}
				}
				//Update the enemies remaining text.
				enemiesRemaining = GameObject.FindGameObjectsWithTag ("Enemy").Length;
				enemiesRemainingText.text = enemiesRemaining.ToString();
				//If there are no more eneimeis, the player wins
				if (enemiesRemaining <= 0) {
					Win ();
				}
			}

		/// <summary>
		/// Make the player shoot.
		/// </summary>
		void Shoot(){
			audioSource.PlayOneShot (gunShot);
			//Create a bullet at the bullet spawn location
			Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
		}

		/// <summary>
		/// Raises the collision enter event.
		/// </summary>
		/// <param name="newCollision">New collision.</param>
		void OnCollisionEnter(Collision newCollision){
			//When the player is on the ground, allow them to jump
			if (newCollision.gameObject.tag == "Ground") {
				canJump = true;
			}
		}

		/// <summary>
		/// Raises the trigger enter event.
		/// </summary>
		/// <param name="newCollider">New collider.</param>
		void OnTriggerEnter(Collider newCollider){
			//If the player collides with an enemy, take a random amount of damage
			if (newCollider.gameObject.tag == "Enemy") {
				takeDamage(Random.Range(10,25),ref health,newCollider);
			}
			//If the player collides with an interactable wall, start the coroutine to move it
			if (newCollider.gameObject.tag == "InteractableWall") {
				StartCoroutine(InteractWall (2,newCollider));
			}
			//If the player collides with a luck boost, enable the luck boost for 10 seconds
			if (newCollider.gameObject.tag == "Luck") {
				StartCoroutine(Luck (10));
				Destroy (newCollider.gameObject);
			}
			//If the player collides with a agility boost, enable the agility boost for 10 seconds
			if (newCollider.gameObject.tag == "Agility") {
				StartCoroutine(Agility (10));
				Destroy (newCollider.gameObject);
			}
		}

		//Enable the luck boost for a certain amount of time then disable it
		IEnumerator Luck(int delay){
			ScoreController.luckBoost = true;
			luckText.text = "LUCK";
			yield return new WaitForSeconds (delay);
			ScoreController.luckBoost = false;
			luckText.text = "";
		}

		//Enable the agility boost for a certain amount of time then disable it
		IEnumerator Agility(int delay){
			agilityBoost = true;
			agilityText.text = "AGILITY";
			jumpHeight = jumpHeight + 2;
			yield return new WaitForSeconds (delay);
			agilityBoost = false;
			jumpHeight = jumpHeight - 2;
			agilityText.text = "";
		}

		//Move the interactable wall down 1.2m for x amount of seconds, then move it back
		public IEnumerator InteractWall(int delay, Collider newCollider){
			newCollider.transform.position = new Vector3 (newCollider.transform.position.x, newCollider.transform.position.y - 1.2f, newCollider.transform.position.z);
			yield return new WaitForSeconds (delay);
			newCollider.transform.position = new Vector3 (newCollider.transform.position.x, newCollider.transform.position.y + 1.2f, newCollider.transform.position.z);
		}

		//Take damage and update the health bar. If the health is 0, the player has dies
		int takeDamage (int damage,ref int currentHealth, Collider col){
			//Vector3 pushDirection;
			currentHealth -= damage;
			healthBar.UpdateBar (currentHealth, 100);
			//pushDirection = col.impulse * -10;
			//rb.AddForce (pushDirection * knockBackForce);
			if (currentHealth <= 0) {
				Dead ();
			}
			return currentHealth;
		}

		//Load the death scene
		void Dead(){
			SceneManager.LoadScene ("Dead");
		}

		//Timer script.
		//delay for 1 second then minus 1 from the time remaining
		//If the timer reaches 0, the player has failed
		public IEnumerator Timer(int delay){
			yield return new WaitForSeconds (delay);
			time--;
			StartCoroutine(Timer(1));
			timerText.text = time.ToString();
			if (time <= 0) {
				Dead ();
			}
		}

		//Load the win scene
		void Win(){
			SceneManager.LoadScene ("Win");
		}
	}
}
