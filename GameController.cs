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
	private bool done = true;

	void Start() {
		init (false);
		done = false;
	}

	public void init (bool justDied) {
		GameObject dividerModel = new GameObject();
		MakeSprite(dividerModel, "Line", transform, .3f, 0, 2, 4, 100);
		dividerModel.name = "Divider";
		captain = new GameObject ();
		captain.AddComponent<CaptainManager> ();
		captain.transform.parent = transform;
		captain.name = "Captain";
		GameObject shipH = new GameObject ();
		shipH.transform.parent = transform;
		shipH.name = "ShipHandler";
		ship = new GameObject ();
		ship.AddComponent<Ship> ();
		MakeSprite (ship, "rocket", shipH.transform, 0, 0, 1, 1, 500);
//		ship.SetActive(true);
//		captain.SetActive(true);
		eMan = gameObject.AddComponent<EnemyManager>();
		eMan.init (this);
		level = 1;
		numLevels = 2;
		//For now let's just worry about loading and executing a single level. Eventually, we will have to be more sophisticated about restarting levels and loading new levels.
		//May not need separate function longterm.
		this.GetInstructions ("JERF/level" + level.ToString()); 		captain.GetComponent<CaptainManager> ().init (this);
		ship.GetComponent<Ship> ().init (this);
		GameObject ProtoShip = new GameObject();
		MakeSprite ( ProtoShip, "ProtoShip", captain.transform, 0, 0, 1, 1, 100);
		ProtoShip.name = "ProtoShip";
		ProtoShip.GetComponent<Renderer> ().sortingLayerName = "Default";
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
		// Asteroids. IMPORTANT, asteroid int values correspond to a percentage frequency for random generation.
		//Therefore if size is 84, then for each frame, if Random.value>.84, generate a random asteroid. Random.value creates a float between 0-1
		if (inst [0] == "A") {
			//Need to cast strings as integers. Args for this eMan instruction are (string type, int size, int x). Type is enemy type
			//Size is squad size (squad implementation is up to you for now, coming in one after another maybe). X is x value of screen descent.
			eMan.getInstruction("A", Int32.Parse(inst[1]), Int32.Parse(inst[2]));
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

	public void MakeModel(GameObject quad, string textureName, Transform parentTransform, float x, float y, float xScale, float yScale) {
		quad.transform.parent = parentTransform;
		quad.transform.localPosition = new Vector3 (x, y, 0);
		quad.transform.localScale = new Vector3 (xScale, yScale, 0);
		quad.name = quad.name + "Model";
		Material mat = quad.GetComponent<Renderer> ().material;
		mat.shader = Shader.Find ("Sprites/Default");
		mat.mainTexture = Resources.Load<Texture2D> ("Textures/" + textureName);
	}

	// fills the passed object with a sprite with the texture 
	public void MakeSprite(GameObject obj, string textureName, Transform parentTransform, 
				float x, float y, float xScale, float yScale, float pixelsPer, params float[] pivot) {
		obj.transform.parent = parentTransform;
		obj.transform.localPosition = new Vector3 (x, y, 0);
		obj.transform.localScale = new Vector3 (xScale, yScale, 1);
		obj.name = textureName + "Sprite";
		SpriteRenderer rend = obj.AddComponent<SpriteRenderer> ();
		Texture2D texture = Resources.Load<Texture2D> ("Textures/" + textureName);
		float xBound = .5f;
		float yBound = .5f;
		if (pivot.Length > 0) {
			xBound = pivot [0];
		} if (pivot.Length > 1) {
			yBound = pivot [1];
		}
		rend.sprite = Sprite.Create(texture, 
			new Rect(0, 0, texture.width, texture.height), 
			new Vector2(xBound, yBound),
			pixelsPer);
		print (obj.name);
	}


	void GetInstructions (string level) {
		iter = 0;
		instructions = Resources.Load<TextAsset>(level).text.Split(new char[1]{'\n'});
		print (instructions[0]);
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

