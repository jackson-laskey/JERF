using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	// the health bar of each component on the RHS; 0 <= .health <= 100
	public ComponentHealth laserLevel;
	public ComponentHealth shieldLevel;
	public ComponentHealth engineLevel;
	public GameObject death;
	private Animator direction;
	public GameObject JET;
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
		transform.localPosition = new Vector3 (-3.2f, -4.2f, 0);
		gameObject.name = "Ship";
		tag = "PlayerController";

		laserLevel = GameObject.Find("Lasers").GetComponentInChildren<ComponentHealth>();
		shieldLevel = GameObject.Find("Shields").GetComponentInChildren<ComponentHealth>();
		engineLevel = GameObject.Find("Engines").GetComponentInChildren<ComponentHealth>();

		direction = this.gameObject.GetComponent<Animator> ();
		//jets = JET.GetComponent<Animator> ();

		//direction.SetInteger ("Direction", 0);
		//jets.SetInteger ("Power", 3);
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
		clock += Time.deltaTime * laserLevel.health;
		// fire lasers if thresholds have been reached- extra-fast firing cycle for laser health == 100
		if (laserLevel.health == 100 && clock > fireInterval / 1.3f) {
			Fire ();
		}
		if (clock > fireInterval) {
			Fire ();
		}

//		if (engineLevel.health >= 50) {
//			jets.SetInteger ("Power", 3);
//		} else if (engineLevel.health < 50) {
//			jets.SetInteger ("Power", 2);
//		} else if (engineLevel.health <= 20) {
//			jets.SetInteger ("Power", 1);
//		}

//		// move left if "a" is being pressed, right if "d" is being pressed. Confined to LHS.
		if (Input.GetKey ("a") && gameObject.transform.position.x > -6) {
//			jets.SetInteger ("Direction", 1);
//			direction.SetInteger ("Direction", 1);
//			JET.transform.localPosition = new Vector3 (.02f, -.37f, 0);
			transform.Translate (-(Time.deltaTime * 5 * (engineLevel.health / 100)), 0, 0);
//
		} else if (Input.GetKey ("d") && gameObject.transform.position.x < -.5) {
//			jets.SetInteger ("Direction", 2);
//			direction.SetInteger ("Direction", 2);
//			JET.transform.localPosition = new Vector3 (-.02f, -.37f, 0);
			transform.Translate (Time.deltaTime * 5 * (engineLevel.health / 100), 0, 0);
		} else {
//			jets.SetInteger ("Direction", 0);
//			direction.SetInteger ("Direction", 0);
//			JET.transform.localPosition = new Vector3 (0, -.37f, 0);
		}
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
		clock = 0;
		GameObject shot = new GameObject();
		shot.transform.parent = transform.parent;
		shot.AddComponent<PlayerLaser> ();
		shot.transform.position = new Vector3(transform.position.x, transform.position.y + .7f);
	}

	private void Die() {
		// send some message to the GameController
		//var x = Instantiate(death ,this.transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

}
