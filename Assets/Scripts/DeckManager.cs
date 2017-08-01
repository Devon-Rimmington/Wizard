using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MaterialUI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeckManager : MonoBehaviour {

	private int[] deckOrder = new int[52];
	private Dictionary<int, int> cardValue;

	private int shuffleAmount = 4;
	private int currentIndex = 0;

	private int previousCardValue, currentCardValue;

	private int currentPlayer = -1;

	private int drinkDrinkNumber;

	private string[] playerNames = null;

	public GameObject restartButton;

	private AndroidJavaClass jc, vibrate;
	private AndroidJavaObject jo;

	private bool isVibrate, isSounds;

	void Awake(){

		if (PlayerPrefs.GetInt ("_vibrate") == 1) {
			isVibrate = true;
		} else {
			isVibrate = false;
		}

		cardValue = new Dictionary<int, int> ();

		// setup the values 
		for(int i = 0; i < 52; i++){
			deckOrder [i] = i;
		}

		// shuffle the cards
		for (int i = 0; i < 52 * shuffleAmount; i++) {
			int index = (int)Mathf.Abs(Mathf.Pow(((Mathf.PI * i) * (deckOrder.Length/Mathf.Abs(Random.insideUnitCircle.x))), Mathf.Abs(Random.insideUnitCircle.y)* shuffleAmount)%52);
			SwapNumbers (i%52, index);
		}

		// setup the dictionary that matches the index of the card in the order with the value of the card
		// refer to the sprite sheet for the order/value relationship
		for (int i = 0; i < 52; i++) {
			cardValue.Add (i, GetCardValue (i));
		}

		previousCardValue = -1;
		currentCardValue = -1;
		currentPlayer = -1;
		drinkDrinkNumber = Random.Range(0, 27);

		isVibrate = true;

	}

	// Use this for initialization
	void Start () {

		// todo remove this
		currentIndex = 0;

		/*
		for (int i = 0; i < 52; i++) {
			Debug.Log (deckOrder[i]);
		}
		*/


		/*
		DialogAlert alert = DialogManager.CreateAlert ();
		alert.bodyText.fontSize = 36;
		alert.bodyText.text = "Play in the order of the names you entered AFTER you have selected an initail card";
		alert.titleSection.text.fontSize = 60;
		alert.titleSection.text.text =  "Tap deck to select an initial card";
		alert.Show ();
		*/


		jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); 
		jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

		vibrate = new AndroidJavaClass ("com.example.wizard.Vibrate");

		Debug.Log ("classes" + jc + " " + jo + " " + vibrate);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SwapNumbers(int i, int index){

		// Debug.Log(i + "\t" + index);

		int temp = deckOrder [i];
		deckOrder [i] = deckOrder [index];
		deckOrder [index] = temp;
	}

	public int GetCardValue(int index){

		if (index == 0 || index == 10 || index == 20 || index == 30) { 			// ace
			return 1;
		} else if (index == 1 || index == 11 || index == 21 || index == 31) { 	// two
			return 2;
		} else if (index == 2 || index == 12 || index == 22 || index == 32) { 	// three
			return 3;
		} else if (index == 3 || index == 13 || index == 23 || index == 33) {	// four
			return 4;
		} else if (index == 4 || index == 14 || index == 24 || index == 34) {	// five
			return 5;
		} else if (index == 5 || index == 15 || index == 25 || index == 35) {	// six
			return 6;
		} else if (index == 6 || index == 16 || index == 26 || index == 36) {	// seven
			return 7;
		} else if (index == 7 || index == 17 || index == 27 || index == 37) {	// eight
			return 8;
		} else if (index == 8 || index == 18 || index == 28 || index == 38) { 	// nine
			return 9;
		} else if (index == 9 || index == 19 || index == 29 || index == 39) {	// ten
			return 10;
		} else if (index == 40 || index == 43  || index == 46 || index == 49) {	// jack
			return 11;
		} else if (index == 41 || index == 44 || index == 47 || index == 50) {	// queen
			return 12;
		} else if (index == 42 || index == 45 || index == 48 || index == 51) {	// king
			return 13;
		}

		return -1;
	}

	// todo when the card index reaches the max the game is over
	// todo go back to the player select screen
	public int getNextCard(){
		int temp = currentIndex;
		currentIndex++;


		if (currentIndex > deckOrder.Length) {
			/*
			DialogAlert alert = DialogManager.CreateAlert ();
			alert.bodyText.fontSize = 36;
			alert.bodyText.text = "You have finished";
			alert.titleSection.text.fontSize = 60;
			alert.titleSection.text.text =  "Done!";
			alert.Show ();
			SceneManager.LoadScene (0);
			*/

			// create a button that allows the player to leave 
			restartButton.SetActive(true);
			return -1;
		}

		return deckOrder[temp];
	}


	public void GoBack(){
		SceneManager.LoadScene (0);
	}

	public void HigherDrinkRuleCheck(int _currentCardValue, int cardIndex){

		if (currentCardValue == -1) {
			currentCardValue = _currentCardValue;
			currentPlayer++;
			return;
		} else {
			previousCardValue = currentCardValue;
			currentCardValue = _currentCardValue;
			currentPlayer++;
		}

		// reset the player index to 0 if it overflows the names
		if (currentPlayer >= playerNames.Length) {
			currentPlayer = 0;
		}

		Debug.Log (playerNames.Length + " " + playerNames[currentPlayer]);

		// if the card is for drink drink then make the player drink drink
		if(cardIndex == drinkDrinkNumber){

			if(isVibrate)
				vibrate.CallStatic ("vibrate", jo);
			else
				Debug.Log ("testing");


			DialogAlert alert = DialogManager.CreateAlert ();
			alert.bodyText.fontSize = 42;
			alert.bodyText.text = playerNames[currentPlayer] + " has become the DRINK DRINK, everyone must drink when " + playerNames[currentPlayer] + " drink.";
			alert.titleSection.text.fontSize = 60;
			alert.titleSection.text.text =  "DRINK DRINK!!!";

			((Text)alert.buttonSection.affirmativeButton.buttonObject.transform.GetComponentInChildren<Text> ()).text = "";
			((Text)alert.buttonSection.dismissiveButton.buttonObject.transform.GetComponentInChildren<Text> ()).text = "";

			alert.Show ();
			return;
		}

		// check the main rules of the game
		if (previousCardValue != -1 && currentCardValue != -1) {
			// the cards are the same and the current player must take two drinks
			if (previousCardValue == currentCardValue) {

				if(isVibrate)
					vibrate.CallStatic ("vibrate", jo);
				else
					Debug.Log ("testing");

				DialogAlert alert = DialogManager.CreateAlert ();
				alert.bodyText.fontSize = 42;
				alert.bodyText.text = playerNames [currentPlayer] + " must drink twice.";
				alert.titleSection.text.fontSize = 60;
				alert.titleSection.text.text =  playerNames[currentPlayer] + " Drinks Twice";
				alert.buttonSection = null;

				((Text)alert.buttonSection.affirmativeButton.buttonObject.transform.GetComponentInChildren<Text> ()).text = "";
				((Text)alert.buttonSection.dismissiveButton.buttonObject.transform.GetComponentInChildren<Text> ()).text = "";

				alert.Show ();
				return;
			}
			// current player much drink
			if (previousCardValue < currentCardValue) {
				
				if (isVibrate)
					vibrate.CallStatic ("vibrate", jo);
				else
					Debug.Log ("testing");

				DialogAlert alert = DialogManager.CreateAlert ();
				alert.bodyText.fontSize = 42;
				alert.bodyText.text = playerNames[currentPlayer] + " must drink.";
				alert.titleSection.text.fontSize = 60;
				alert.titleSection.text.text =  playerNames[currentPlayer] + " Drinks";

				((Text)alert.buttonSection.affirmativeButton.buttonObject.transform.GetComponentInChildren<Text> ()).text = "";
				((Text)alert.buttonSection.dismissiveButton.buttonObject.transform.GetComponentInChildren<Text> ()).text = "";

				alert.buttonSection = null;
				alert.Show ();
				return;
			}
		}
	}

	public string[] getPlayerNames(){
		return playerNames;
	}

	public void setPlayerNames(string[] names){
		playerNames = names;
	}

	public string getCurrentPlayer(){
		if(currentPlayer < 8 && currentPlayer >= 0)
			return playerNames [currentPlayer-1];
		return "No ones";
	}

	public void toggleVibrate(){
		isVibrate = !isVibrate;
	}

}
