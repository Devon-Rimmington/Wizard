using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchClassic : MonoBehaviour {

	public GameObject objectToTouch, prefab, prefabSpawnPoint;

	public bool allowNextCard;

	private StopClassicCards stopClassicCards;

	// Use this for initialization
	void Start () {
		// allowNextCard = true;
		stopClassicCards = GameObject.Find("ClassicMode").GetComponent<StopClassicCards>();
		Debug.Log (stopClassicCards);
		// stopClassicCards.allowNextCard ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount >= 1) {
			// Debug.Log ("Touched");
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				// Debug.Log ("Touch Phase Begin");
				CheckTouch (Input.GetTouch (0).position);
			}
		}
	}

	void CheckTouch(Vector2 touchPosition){

		Vector3 worldPosition = Camera.main.ScreenToWorldPoint (touchPosition);
		Vector2 position = new Vector2 (worldPosition.x, worldPosition.y);
		Collider2D collider = Physics2D.OverlapPoint (position);

		if (collider == null)
			return;

		if (collider.gameObject == objectToTouch && stopClassicCards.allowCards) {

			GameObject cardNew;

			// create a new card and animate it to be shown
			cardNew = GameObject.Instantiate (prefab, prefabSpawnPoint.transform.position, prefabSpawnPoint.transform.rotation);
			cardNew.SetActive (true);
			cardNew.GetComponent<Animator> ().SetBool ("show", true);
			// cardNew.GetComponent<DeckOfCards> ().ChangeSpriteLayer (3);

			stopClassicCards.blockNextCard ();

			Destroy (gameObject);
		}
	}
}
