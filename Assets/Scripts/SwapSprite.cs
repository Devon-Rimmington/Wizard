using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSprite : MonoBehaviour {

	public LoadSprites loadSprites;
	public DeckManager deckManager;
	public OnTouch touch;
	public bool classicMode;
	private StopClassicCards stopClassicCards;

	// Use this for initialization
	void Start () {
		if (classicMode) {
			stopClassicCards = GameObject.Find("ClassicMode").GetComponent<StopClassicCards>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeSprite(int index){
		int temp = deckManager.getNextCard ();

		if (temp == -1)
			return;
		
		// Debug.Log (temp);
		gameObject.GetComponent<SpriteRenderer> ().sprite = loadSprites.getSprite (temp);

		// Debug.Log(deckManager.GetCardValue(temp));

		deckManager.HigherDrinkRuleCheck (deckManager.GetCardValue(temp), temp);

	}

	public void allowNextCard(){
		if(!classicMode)
			touch.allowNextCardTrue ();
		else {
			stopClassicCards.allowNextCard ();
		}
	}

	public void changeSpriteClassic(int index){
		
	}

	public void removeCard(){
		Destroy (gameObject);
	}

}