using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVibrate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void toggleVibrate(bool vibrate){
		if(vibrate)
			PlayerPrefs.SetInt ("_vibrate", 1);
		else
			PlayerPrefs.SetInt ("_vibrate", 0);
	}
}
