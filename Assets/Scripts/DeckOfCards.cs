using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckOfCards : MonoBehaviour {

	public LoadSprites spriteLoader;
	// the style of deck
	private int _type;

	private SpriteRenderer sprite;


	void Awake(){
		// load the deck style
		_type = PlayerPrefs.GetInt ("_type", 0);
	}

	// Use this for initialization
	void Start () {
		sprite = gameObject.AddComponent<SpriteRenderer> () as SpriteRenderer;
		sprite.sprite = spriteLoader.getSprite (_type);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeSuite(){
		_type = PlayerPrefs.GetInt ("_type", 0);
		// sprite = gameObject.AddComponent<SpriteRenderer> () as SpriteRenderer;
		// sprite.sprite = spriteLoader.getSprite (_type);
	}

	public void ChangeSpriteLayer(int layer){
		gameObject.GetComponent<SpriteRenderer> ().sortingOrder = layer;
	}
}
