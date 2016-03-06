﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScreenController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (GUI.Button(new Rect(Screen.width/2 - 40, Screen.height/2 -40, 100,50), "Start Game")){
			Application.LoadLevel ("Mechanic Study");
		}
		if (GUI.Button (new Rect (Screen.width/2 - 40, Screen.height/2+15, 100, 50), "Instructions")) {

		}
		if (GUI.Button (new Rect (Screen.width/2 -40, Screen.height/2 + 70, 100, 25), "Quit")) {
			Application.Quit();
		}
	}
}
