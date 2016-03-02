//Dear Ryan and Felipe... Thanks for bailing me out on this. I'll make it up to you in code or drinks
//Refer to the Level Input Language for questions about the LIL. I'll spruce it up to make it clearer.

using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	//I'll be using these in code to refer to these objects, but how to instantiate them and resulting changes will be up to you
	public EnemyManager eMan;
	public GameObject ship;
	public GameObject captain;
	public int level;
	public int numLevels;
	private string[] instructions;
	private int iter = 0;
	private bool waiting = false;
	private bool done = false;

	void Start() {
		init (false);
	}

	public void init (bool justDied) {
		ship.SetActive(true);
		captain.SetActive(true);
		eMan = gameObject.AddComponent<EnemyManager>();
		eMan.init (this);
		level = 1;
		numLevels = 2;
		this.GetInstructions ("JERF/level" + level.ToString()); //For now let's just worry about loading and executing a single level. Eventually, we will have to be more sophisticated about restarting levels and loading new levels. May not need separate function longterm.
	}

	void Update() {//Needed an update to handle waiting. Checks if waiting once per frame instead of on infinite loop which crashes
		if (!done) {
			if (instructions [iter] != "X" && !waiting) { // Check if done/not waiting
				ExecuteInstruction (instructions [iter].Split (':')); // Execute the instruction
				iter++;//iterate
			} else if(!waiting){//Means done, not just waiting
				if (++level > numLevels) {
					done = true;
				} else {
					EndLevel (level);
					this.GetInstructions ("JERF/level" + level.ToString ());
					done = false;
				}
			}
		}

	}


	void ExecuteInstruction(string[] inst){
		if (inst [0] == "A") { // Asteroids. IMPORTANT, asteroid int values correspond to a percentage frequency for random generation. Therefore if size is 84, then for each frame, if Random.value>.84, generate a random asteroid. Random.value creates a float between 0-1
			eMan.getInstruction("A", Int32.Parse(inst[1]), Int32.Parse(inst[2])); //Need to cast strings as integers. Args for this eMan instruction are (string type, int size, int x). Type is enemy type. Size is squad size (squad implementation is up to you for now, coming in one after another maybe). X is x value of screen descent.
			if (inst.Length > 3) {
				StartCoroutine (sleep(Int32.Parse(inst[3])));//Starts a routine that sets waiting to true and waits for arg seconds before flipping the bool back to false.
			}
		} else if (inst [0] == "L") { // Light Enemies
			eMan.getInstruction("L", Int32.Parse(inst[1]), Int32.Parse(inst[2]));
			if (inst.Length > 3) {
				StartCoroutine (sleep(Int32.Parse(inst[3])));
			}
		} else if (inst [0] == "H") { // Heavy Enemies
			eMan.getInstruction("H", Int32.Parse(inst[1]), Int32.Parse(inst[2]));
			if (inst.Length > 3) {
				StartCoroutine (sleep(Int32.Parse(inst[3])));
			}
		} else { //Integer for Waiting
			StartCoroutine (sleep(Int32.Parse(inst[0])));
		}
	}

	void GetInstructions (string level) {
		iter = 0;
		instructions = Resources.Load<TextAsset>(level).text.Split(new char[1]{'\n'});
	}

	void EndLevel (int level) {
		StartCoroutine (sleep (10));
	}

	IEnumerator sleep(int time){
		waiting = true;
		yield return new WaitForSeconds(1.0f*time); //Right now waiting for 5 seconds. Eventually, wait for 1.0f*time seconds.
		waiting = false;
    }
		
	void ParseInstruction () {
	}
}

