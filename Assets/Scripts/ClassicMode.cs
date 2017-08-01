using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicMode : MonoBehaviour {

	private int lastNumber, currentNumber, drinkDrink;
	public DeckManager deckManager;

	// Use this for initialization
	void Start () {
		lastNumber = -1;
		currentNumber = -1;
		drinkDrink = Random.Range(0, 27);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetNextCard(){
		int index = deckManager.getNextCard();
		int value = deckManager.GetCardValue (index);

		Result (value, index);
	}

	public void Result(int value, int index){

		if (currentNumber == -1) {
			currentNumber = value;
			return;
		} else {
			
		}

		if (drinkDrink == index) {
			return;
		}

		if (lastNumber == currentNumber) {
		
		} else if (lastNumber < currentNumber) {
		
		}

	}
}
