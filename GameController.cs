//Dear Ryan and Felipe... Thanks for bailing me out on this. I'll make it up to you in code or drinks
//Refer to the Level Input Language for questions about the LIL. I'll spruce it up to make it clearer.

using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	//I'll be using these in code to refer to these objects, but how to instantiate them and resulting changes will be up to you
	public EnemyManager eMan;
	public GameObject ship;
	public GameObject captain;
	public GameObject jets;
	public GameObject leftShield;
	public int level = 1;
	public int numLevels = 12;
	private string[] instructions;
	private int iter = 0;
	private bool waiting = false;
	private bool done = true;
	Sprite[] stextures;
	public Text levelCount;
	public Button restart;
	public bool isDead;
	int levelStartWait = 3;
	public AudioClip DeathSound;

	private bool wInstruction;

//	private bool waiting;

	void Start() {
		DeathSound = Resources.Load ("Sounds/death") as AudioClip;

		levelCount.text = "";
		init (false);
		done = false;
	}

	public void init (bool justDied) {
		isDead = false;
		wInstruction = false;
		if (!justDied) {
			level = 1;
			numLevels = 12;
			StartCoroutine (sleep (levelStartWait));
			stextures = Resources.LoadAll<Sprite> ("Textures/Ship Sprite Sheet");
			captain = new GameObject ();
			captain.AddComponent<CaptainManager> ();
			captain.transform.parent = transform;
			captain.name = "Captain";
			GameObject shipH = new GameObject ();
			shipH.transform.parent = transform;
			shipH.transform.localPosition = new Vector3 (0, 0, 0);
			shipH.name = "ShipHandler";
			ship = new GameObject ();
			ship.AddComponent<Ship> ();
			MakeSprite (ship, stextures [11], shipH.transform, 0, 0, 1, 1, 500);
			ship.AddComponent<Animator> ();
			Animator animator = ship.GetComponent<Animator> ();
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Ship_Animation_Controller");
			ship.transform.localScale = new Vector3 (2, 2, 0);
			jets = new GameObject ();
			stextures = Resources.LoadAll<Sprite> ("Textures/Ship Effects Sheet");
			MakeSprite (jets, stextures [14], ship.transform, 0, -.38f, 1, 1, 100);
			jets.name = "Jets";
			jets.AddComponent<Animator> ();
			animator = jets.GetComponent<Animator> ();
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Jet_Animation_Controller");
			leftShield = new GameObject ();
			MakeSprite (leftShield, stextures [26], ship.transform, 0, 0, 1, 1, 500);
			leftShield.AddComponent<Animator> ();
			animator = leftShield.GetComponent<Animator> ();
			Material colorShield = leftShield.GetComponent<Renderer> ().material;
			colorShield.color = new Color (colorShield.color.a, colorShield.color.g, colorShield.color.b, .40f);
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Left_Shield");
			leftShield.transform.localScale = new Vector3 (1, 1, 0);
			leftShield.name = "LeftShield";
			leftShield.GetComponent<Renderer> ().sortingOrder = 1;
//		ship.SetActive(true);
//		captain.SetActive(true);
			eMan = gameObject.AddComponent<EnemyManager> ();
			eMan.init (this);
			//For now let's just worry about loading and executing a single level. Eventually, we will have to be more sophisticated about restarting levels and loading new levels.
			//May not need separate function longterm.
			this.GetInstructions ("JERF/level" + level.ToString ());
			captain.GetComponent<CaptainManager> ().init (this);
			ship.GetComponent<Ship> ().init (this, restart);
			GameObject ProtoShip = new GameObject ();
			stextures = Resources.LoadAll<Sprite> ("Textures/Captain_Effects_Sheet_2");

			//for(int ii=0; ii< snames.Length; ii++) {
			//	snames[ii] = stextures[ii].name;
			//}
			MakeSprite (ProtoShip, stextures [0], captain.transform, 0, 0, 1, 1, 100);
			ProtoShip.name = "ProtoShip";
			ProtoShip.GetComponent<Renderer> ().sortingLayerName = "Default";

			setLevelText ();


			GameObject Starspawner = new GameObject ();
			StarSpawner spawner = Starspawner.AddComponent<StarSpawner> ();
			spawner.init (eMan);


		

		} else {
			Reload ();

			restart.gameObject.SetActive (false);
			stextures = Resources.LoadAll<Sprite> ("Textures/Ship Sprite Sheet");
			captain = new GameObject ();
			captain.AddComponent<CaptainManager> ();
			captain.transform.parent = transform;
			captain.name = "Captain";
			GameObject shipH = new GameObject ();
			shipH.transform.parent = transform;
			shipH.transform.localPosition = new Vector3 (0, 0, 0);
			shipH.name = "ShipHandler";
			ship = new GameObject ();
			ship.AddComponent<Ship> ();
			MakeSprite (ship, stextures [11], shipH.transform, 0, 0, 1, 1, 500);
			ship.AddComponent<Animator> ();
			Animator animator = ship.GetComponent<Animator> ();
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Ship_Animation_Controller");
			ship.transform.localScale = new Vector3 (2, 2, 0);
			jets = new GameObject ();
			stextures = Resources.LoadAll<Sprite> ("Textures/Ship Effects Sheet");
			MakeSprite (jets, stextures [14], ship.transform, 0, -.38f, 1, 1, 100);
			jets.name = "Jets";
			jets.AddComponent<Animator> ();
			animator = jets.GetComponent<Animator> ();
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Jet_Animation_Controller");
			leftShield = new GameObject ();
			MakeSprite (leftShield, stextures [26], ship.transform, 0, 0, 1, 1, 500);
			leftShield.AddComponent<Animator> ();
			animator = leftShield.GetComponent<Animator> ();
			Material colorShield = leftShield.GetComponent<Renderer> ().material;
			colorShield.color = new Color (colorShield.color.a, colorShield.color.g, colorShield.color.b, .40f);
			animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Left_Shield");
			leftShield.transform.localScale = new Vector3 (1, 1, 0);
			leftShield.name = "LeftShield";
			leftShield.GetComponent<Renderer> ().sortingOrder = 1;
			//		ship.SetActive(true);
			//		captain.SetActive(true);
			eMan = gameObject.AddComponent<EnemyManager> ();
			eMan.init (this);
			//For now let's just worry about loading and executing a single level. Eventually, we will have to be more sophisticated about restarting levels and loading new levels.
			//May not need separate function longterm.
			this.GetInstructions ("JERF/level" + level.ToString ());
			captain.GetComponent<CaptainManager> ().init (this);
			ship.GetComponent<Ship> ().init (this, restart);
			GameObject ProtoShip = new GameObject ();
			stextures = Resources.LoadAll<Sprite> ("Textures/Captain_Effects_Sheet_2");
			//for(int ii=0; ii< snames.Length; ii++) {
			//	snames[ii] = stextures[ii].name;
			//}
			MakeSprite (ProtoShip, stextures [0], captain.transform, 0, 0, 1, 1, 100);
			ProtoShip.name = "ProtoShip";
			ProtoShip.GetComponent<Renderer> ().sortingLayerName = "Default";

			setLevelText ();


	}

	}

	void Update() {//Needed an update to handle waiting. Checks if waiting once per frame instead of on infinite loop which crashes

		if (isDead) {
			AudioSource.PlayClipAtPoint(DeathSound,transform.position);
			return;
		}
		if (wInstruction && !waiting) {
			if (GameObject.FindObjectsOfType<ParentEnemy> ().Length == 0) {
				wInstruction = false;
			} else {
				return;
			}
		}
		if (!done) {
			if (instructions [iter] != "X" && !waiting) { // Check if done/not waiting
				ExecuteInstruction (instructions [iter].Split (':')); // Execute the instruction
				levelCount.text = "";
				iter++;//iterate
				if (instructions [iter] == "X") {
					StartCoroutine (sleep (1));
				}
			} else if(!waiting && GameObject.FindObjectsOfType<ParentEnemy>().Length == 0 && eMan.spawningA == false){//Means done, not just waiting
				if (++level > numLevels) {
						endText ();
					done = true;
				} else {
					setLevelText ();
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
		if (inst[0] == "W") {
			wInstruction = true;
			StartCoroutine (sleep (1));
		} else if (inst [0] == "A" || inst [0] == "L" || inst [0] == "P1" || inst [0] == "P2" || inst [0] == "P3") {
			//Need to cast strings as integers. Args for this eMan instruction are (string type, int size, int x). Type is enemy type
			//Size is squad size (squad implementation is up to you for now, coming in one after another maybe). X is x value of screen descent.
			if (inst.Length == 3) {
				eMan.getInstruction (inst [0], Int32.Parse (inst [1]), Int32.Parse (inst [2]), 0);
			} else if (inst.Length == 2) {
				eMan.getFormation (inst [0], Int32.Parse (inst [1]));
			}
		} else if (inst [0] == "BOSS") {
			eMan.getInstruction (inst [0], 0, 0, 0);
		} else if (inst [0] == "S" || inst [0] == "B") { // Heavy Enemies
			eMan.getInstruction (inst [0], Int32.Parse (inst [1]), Int32.Parse (inst [2]),0);
		} else if (inst [0] == "H") {
			if (inst.Length > 3) {
				eMan.getInstruction (inst [0], Int32.Parse (inst [1]), Int32.Parse (inst [2]), Int32.Parse (inst [3]));
			} else {
				eMan.getInstruction (inst [0], Int32.Parse (inst [1]), Int32.Parse (inst [2]), UnityEngine.Random.Range(0, 4));
			}
		}
		else { //Integer for Waiting
			StartCoroutine (sleep(Int32.Parse(inst[0])));
		}
	}

//	public void MakeModel(GameObject quad, string textureName, Transform parentTransform, float x, float y, float xScale, float yScale) {
//		quad.transform.parent = parentTransform;
//		quad.transform.localPosition = new Vector3 (x, y, 0);
//		quad.transform.localScale = new Vector3 (xScale, yScale, 0);
//		quad.name = quad.name + "Model";
//		Material mat = quad.GetComponent<Renderer> ().material;
//		mat.shader = Shader.Find ("Sprites/Default");
//		mat.mainTexture = Resources.Load<Texture2D> ("Textures/" + textureName);
//	}

	// fills the passed object with a sprite with the texture 
	public void MakeSprite(GameObject obj, string textureName, Transform parentTransform, 
				float x, float y, float xScale, float yScale, float pixelsPer, params float[] pivot) {
		obj.transform.parent = parentTransform;
		obj.transform.localPosition = new Vector3 (x, y, 0);
		obj.transform.localScale = new Vector3 (xScale, yScale, 1);
		obj.name = textureName + "Sprite";
		SpriteRenderer rend = obj.AddComponent<SpriteRenderer> ();
		Sprite texture = Resources.Load<Sprite> ("Textures/" + textureName);
		float xBound = .5f;
		float yBound = .5f;
		if (pivot.Length > 0) {
			xBound = pivot [0];
		} if (pivot.Length > 1) {
			yBound = pivot [1];
		}
		rend.sprite = texture;
	}

	public void MakeSprite(GameObject obj, Sprite s, Transform parentTransform, 
		float x, float y, float xScale, float yScale, float pixelsPer, params float[] pivot) {
		obj.transform.parent = parentTransform;
		obj.transform.localPosition = new Vector3 (x, y, 0);
		obj.transform.localScale = new Vector3 (xScale, yScale, 1);
		obj.name = s.name + "Sprite";
		SpriteRenderer rend = obj.AddComponent<SpriteRenderer> ();
		Sprite texture = s;
		float xBound = .5f;
		float yBound = .5f;
		if (pivot.Length > 0) {
			xBound = pivot [0];
		} if (pivot.Length > 1) {
			yBound = pivot [1];
		}
		rend.sprite = texture;
	}

	void setLevelText(){
		levelCount.text = "Level: " + level;
	}

	void endText(){
		levelCount.text = "Sector Cleared, You Win!";
	}

	void GetInstructions (string level) {
		iter = 0;
		instructions = Resources.Load<TextAsset>(level).text.Split(new char[1]{'\n'});
	}

	void EndLevel (int level) {
		StartCoroutine (sleep (levelStartWait));
	}

	void Reload() {
		Destroy(GameObject.Find("CaptainDeath"));
		Destroy(GameObject.Find("Death"));
		setLevelText ();
		StartCoroutine (sleep (3));
	}

	public void DestroyEnemies() {
		foreach (ParentEnemy i in GameObject.FindObjectsOfType<ParentEnemy> ()) {
			Destroy (i.gameObject);
		}
		foreach (Spawner i in GameObject.FindObjectsOfType<Spawner> ()) {
			Destroy (i.gameObject);
		}
		eMan.spawningA = false;
		isDead = true;
	}

	public void Quit(){
		Application.Quit ();
	}

	public void Menu(){
		Application.LoadLevel ("Start Screen");
	}

	IEnumerator sleep(int time){
		waiting = true;
		yield return new WaitForSeconds(1.0f*time); //Right now waiting for 5 seconds. Eventually, wait for 1.0f*time seconds.
		waiting = false;
    }
		
//	bool Wait() {
//		if (
}

