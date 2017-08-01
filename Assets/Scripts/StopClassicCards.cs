using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopClassicCards : MonoBehaviour {

	public bool allowCards;

	// Use this for initialization
	void Start () {
		allowCards = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void allowNextCard(){
		allowCards = true;
	}

	public void blockNextCard(){
		allowCards = false;
	}
}
