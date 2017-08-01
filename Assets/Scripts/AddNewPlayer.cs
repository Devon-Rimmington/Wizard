using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddNewPlayer : MonoBehaviour {

	private int numberOfPlayers = 0, maxNumberOfPlayers = 8;
	public GameObject inputPrefab, parent;
	public DeckManager deckManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addNewPlayer(string input){

		if (input.Length == 0 && numberOfPlayers == 0) { 		// if there is no player added leave the only input
			return;
		} else if (input.Length == 0 && numberOfPlayers != 0) { // if there are fields but one is set to be empty remove it because their will be no player to play with
			// numberOfPlayers--;
			// remove the parent because that's what holds the text input
			Destroy (gameObject);
			return;
		}

		if (numberOfPlayers < maxNumberOfPlayers) {
			GameObject newInput = GameObject.Instantiate (inputPrefab, parent.transform.position, parent.transform.rotation);
			// set the parent
			newInput.transform.parent = parent.transform;
			// reset the scale because that gets fucked up for some reason
			newInput.transform.localScale = gameObject.transform.localScale;
			// set the input field to be blank for the next player
			newInput.GetComponent<InputField> ().text = "";
			// increase the number of players
			numberOfPlayers++;
		} else {
			// warn the player that there is no room left for more players

		}
	}
}
