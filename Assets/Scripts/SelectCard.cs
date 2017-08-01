using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCard: MonoBehaviour {

	public GameObject objectToTouch;
	public int _type;
	public DeckOfCards deckOfCards;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeDeck(){
		PlayerPrefs.SetInt ("_type", _type);
		deckOfCards.ChangeSuite ();
	}
}
