using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSprites : MonoBehaviour {

	private Sprite[] sprites;
	public string folder = "SpriteSheets/", spriteGroup = "CardFrontSpriteSheet";

	void Awake(){
		sprites = Resources.LoadAll<Sprite> (folder + spriteGroup);
	}

	// Use this for initialization
	void Start () {
		sprites = Resources.LoadAll<Sprite> (folder + spriteGroup);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Sprite getSprite(int index){

		if (index >= sprites.Length)
			return null;
		else if (index < 0) {
			return null;
		}

		return sprites [index];
	}
}
