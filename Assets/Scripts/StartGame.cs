using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	public GameObject playerSelect, mainMenuHolder, MainGame, ClassicGame;
	public DeckManager deckManager;

	private bool classicMode;

	void Awake(){
		classicMode = true;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void hideMenu(){
		mainMenuHolder.GetComponent<Animator> ().SetBool ("hide", true);
		playerSelect.GetComponent<Animator> ().SetBool ("hide", false);
	}

	public void showMenu(){
		mainMenuHolder.GetComponent<Animator> ().SetBool ("hide", false);
		playerSelect.GetComponent<Animator> ().SetBool ("hide", true);
	}


	public void Begin(){

		GameObject[] playerNames = GameObject.FindGameObjectsWithTag("PlayerNames");

		if (playerNames.Length > 1) {
			// start the game
		
			string[] names = null;

			if (playerNames.Length != 8) {
				names = new string[playerNames.Length - 1];
				// collect the names of the players and set them in the controller
				for (int i = 0; i < playerNames.Length; i++) {
					if (!playerNames [i].GetComponent<InputField> ().text.Equals (""))
						names [i] = playerNames [i].GetComponent<InputField> ().text;
				}
			} else {
				names = new string[playerNames.Length];
				// collect the names of the players and set them in the controller
				for (int i = 0; i < playerNames.Length; i++) {
					if (!playerNames [i].GetComponent<InputField> ().text.Equals (""))
						names [i] = playerNames [i].GetComponent<InputField> ().text;
				}
			}

			deckManager.setPlayerNames (names);
			playerSelect.GetComponent<Animator> ().SetBool ("hide", true);
		}else{
			// warn the player that there is no one in the game yet

			Debug.Log ("not enough players");
		}
	}

	public void ShowGame(){

		Debug.Log ("showing games");

		if (classicMode) {
			if (gameObject.GetComponent<Animator> ().GetBool ("hide"))
				MainGame.SetActive (true);
		} else {
			if (gameObject.GetComponent<Animator> ().GetBool ("hide"))
				ClassicGame.SetActive (true);
		}
	}

	public void ToggleClassicMode(bool _classicMode){
		// Debug.Log (_classicMode);
		classicMode = !classicMode;
	}
}
