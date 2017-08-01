using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnTouch : MonoBehaviour {

	public GameObject objectToTouch;
	private Animator animator;

	public GameObject prefab;
	private GameObject cardOut = null, cardNew = null;

	public bool allowNextCard;


	// Use this for initialization
	void Start () {
		allowNextCard = true;	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount >= 1 && allowNextCard) {
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
		
		if (collider.gameObject == objectToTouch) {
			// animator.SetBool ("show", true);

			allowNextCard = false;

			if (cardNew == null) {

				// create a new card and animate it to be shown
				cardNew = GameObject.Instantiate (prefab, objectToTouch.transform.position, gameObject.transform.rotation);
				cardNew.SetActive (true);
				cardNew.GetComponent<Animator> ().SetBool ("show", true);

				// if there is a card on the way out set that to exit the screen
				if (cardOut != null) {
					cardOut.GetComponent<Animator> ().SetBool ("hide", true);
				}
			} else {

				cardOut = cardNew;
				cardOut.GetComponent<Animator> ().SetBool ("hide", true);

				// create a new card and animate it to be shown
				cardNew = GameObject.Instantiate (prefab, objectToTouch.transform.position, gameObject.transform.rotation);
				cardNew.SetActive (true);
				cardNew.GetComponent<Animator> ().SetBool ("show", true);
			}

			// Debug.Log("Touched");
		}
	}

	public void allowNextCardTrue(){
		allowNextCard = true;
	}


	public void allowNextCardFalse(){
		allowNextCard = false;
	}

}
