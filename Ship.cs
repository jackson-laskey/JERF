﻿using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	// the health bar of each component on the RHS; 0 <= .health <= 100
	public LaserHealth laserLevel;
	public ComponentHealth shieldLevel;
	public ComponentHealth engineLevel;
	public GameObject death;
<<<<<<< HEAD
	public GameObject Jet;
	private Animator animator;
=======
	private Animator direction;
	public GameObject JET;
>>>>>>> PrefabsToCode
	private Animator jets;


	// reference to GameController script
	public GameController controller;

	// tracks frames to determine frequency of laser launch
	private float clock;
	// when clock reaches threshold, fires and restarts clock
	private float fireInterval;

	private bool initd = false;



	public void init (GameController gContr) {
		controller = gContr;
		gameObject.AddComponent<Rigidbody2D> ().isKinematic = true;
		gameObject.AddComponent<BoxCollider2D> ().isTrigger = true;
		transform.localPosition = new Vector3 (-3.2f, -4, 0);
		gameObject.name = "Ship";
		tag = "PlayerController";

		laserLevel = GameObject.Find("Lasers").GetComponentInChildren<LaserHealth>();
		shieldLevel = GameObject.Find("Shields").GetComponentInChildren<ComponentHealth>();
		engineLevel = GameObject.Find("Engines").GetComponentInChildren<ComponentHealth>();
<<<<<<< HEAD
		animator = this.GetComponent<Animator> ();
		animator.SetInteger ("Direction", 0);
		jets = Jet.GetComponent<Animator> ();
		jets.SetInteger ("Direction", 0);
		jets.SetInteger ("Power", 3);
=======
		JET = GameObject.Find ("Jets");
		direction = this.gameObject.GetComponent<Animator> ();
//		jets = JET.GetComponent<Animator> ();

<<<<<<< HEAD
>>>>>>> PrefabsToCode


		direction.SetInteger ("Direction", 0);
		jets.SetInteger ("Power", 3);
=======
//		direction.SetInteger ("Direction", 0);
//		jets.SetInteger ("Power", 3);
>>>>>>> origin/PrefabsToCode
		// loads template for laser prefab instantiation


		// clock tracks time passed, ship fires when clock passes threshold. clock then resets.
		clock = 0;
		fireInterval = 32f;

		initd = true;
	}
	
//	 Update is called once per frame
	void Update () {
		if (!initd) {
			return;
		}
		// clock increment is modified by laser health so fire rate is proportional to laser health
		//print(laserLevel.health);
		// fire lasers if thresholds have been reached- extra-fast firing cycle for laser health == 100
		if (Input.GetKeyDown(KeyCode.Space) && laserLevel.health>0) {
			Fire ();
		}

		if (engineLevel.health >= 90) {
			jets.SetInteger ("Power", 4);
		}
		else if (engineLevel.health >= 50) {
			jets.SetInteger ("Power", 3);
		} else if (engineLevel.health < 50) {
			jets.SetInteger ("Power", 2);
		} else if (engineLevel.health <= 20) {
			jets.SetInteger ("Power", 1);
		}

<<<<<<< HEAD
		if (engineLevel.health >= 50) {
			
			jets.SetInteger ("Power", 3);
		}
		else if (engineLevel.health < 50 ) {
			
			jets.SetInteger ("Power", 2);
		} else if (engineLevel.health <= 20) {
			jets.SetInteger ("Power", 1);
		}


		// move left if "a" is being pressed, right if "d" is being pressed. Confined to LHS.
		if (Input.GetKey ("a") && gameObject.transform.position.x > -6) {
			animator.SetInteger ("Direction", 1);
			jets.SetInteger ("Direction", 1);


			transform.Translate (-(Time.deltaTime * 5 * (engineLevel.health / 100)), 0, 0);
		} else if (Input.GetKey ("d") && gameObject.transform.position.x < -.5) {
			animator.SetInteger ("Direction", 2);
			jets.SetInteger ("Direction", 2);

			transform.Translate (Time.deltaTime * 5 * (engineLevel.health / 100), 0, 0);

		} else {
			animator.SetInteger ("Direction", 0);
			jets.SetInteger ("Direction", 0);
		}


=======
//		// move left if "a" is being pressed, right if "d" is being pressed. Confined to LHS.
		if (Input.GetKey ("a") && gameObject.transform.position.x > -6) {
			jets.SetInteger ("Direction", 1);
			direction.SetInteger ("Direction", 1);
			JET.transform.localPosition = new Vector3 (.02f, -.37f, 0);
			transform.Translate (-(Time.deltaTime * 5 * (engineLevel.health / 100)), 0, 0);

		} else if (Input.GetKey ("d") && gameObject.transform.position.x < -.5) {
			jets.SetInteger ("Direction", 2);
			direction.SetInteger ("Direction", 2);
			JET.transform.localPosition = new Vector3 (-.02f, -.37f, 0);
			transform.Translate (Time.deltaTime * 5 * (engineLevel.health / 100), 0, 0);
		} else {
			jets.SetInteger ("Direction", 0);
			JET.transform.localPosition = new Vector3 (0, -.38f, 0);
			direction.SetInteger ("Direction", 0);
        }
>>>>>>> PrefabsToCode
	}

	// Handles hits
	void OnTriggerEnter2D(Collider2D coll) {
		// different cases for different objects; mostly they just damage the ship
		switch (coll.name) {
		case "Asteroid":
			if (shieldLevel.Damage (49)) {
				Die ();
			}
			break;
		case "Laser":
			if (shieldLevel.Damage (15)) {
				Die ();
			}
			break;
		case "SmallEnemy":
			if (shieldLevel.Damage (30)) {
				Die ();
			}
			break;
		default:
			break; 
		}
	}

//	protected void MakeModel() {
//		model = GameObject.CreatePrimitive (PrimitiveType.Quad);
//		model.transform.parent = gameObject.transform;
//		model.transform.localPosition = new Vector3 (0, 0, 0);
//		model.transform.localScale = transform.localScale;
//		model.name = "ShipModel";
//		Texture2D[] ShipTextures = Resources.LoadAll<Texture2D> ("Textures/ShipSprites");
//		print (ShipTextures);
//		Material mat = model.GetComponent<Renderer> ().material;
//		mat.shader = Shader.Find ("Sprites/Default");
//		mat.mainTexture = ShipTextures[0];
//	}

	private void Fire() {
		if (laserLevel.health < 50) {
			GameObject shot = new GameObject ();
			shot.transform.parent = transform.parent;
			shot.AddComponent<PlayerLaser> ();
			shot.transform.position = new Vector3 (transform.position.x, transform.position.y + .7f);
		} else if (laserLevel.health < 100) {
			GameObject shot = new GameObject ();
			shot.transform.parent = transform.parent;
			shot.AddComponent<PlayerLaser> ();
			shot.transform.position = new Vector3 (transform.position.x-.1f, transform.position.y + .7f);
			GameObject shot2 = new GameObject ();
			shot2.transform.parent = transform.parent;
			shot2.AddComponent<PlayerLaser> ();
			shot2.transform.position = new Vector3 (transform.position.x+.1f, transform.position.y + .7f);
		} else {
			GameObject shot = new GameObject ();
			shot.transform.parent = transform.parent;
			shot.AddComponent<SuperPlayerLaser> ();
			shot.transform.position = new Vector3 (transform.position.x-.1f, transform.position.y + .7f);
			GameObject shot2 = new GameObject ();
			shot2.transform.parent = transform.parent;
			shot2.AddComponent<SuperPlayerLaser> ();
			shot2.transform.position = new Vector3 (transform.position.x+.1f, transform.position.y + .7f);
		}
		laserLevel.fire ();
	}

	private void Die() {
		// send some message to the GameController
<<<<<<< HEAD
		var x = Instantiate(death, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
			Destroy (gameObject);
=======

		GameObject death = new GameObject ();
		controller.MakeSprite (death,"", GameObject.Find("ShipHandler").transform, 0, 0, 1, 1, 500);
		death.transform.localPosition = this.transform.position;
		death.AddComponent<Animator> ();
		Animator animator = death.GetComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/ship_Death_Controller");
		death.transform.localScale = new Vector3 (2, 2, 0);

		GameObject.Find ("Captain").GetComponent<CaptainManager> ().Die ();

//		var x = Instantiate(death ,this.transform.position, Quaternion.identity);
		Destroy (gameObject);

>>>>>>> PrefabsToCode
	}

}
