using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	// the health bar of each component on the RHS; 0 <= .health <= 100
	public ComponentHealth laserLevel;
	public ComponentHealth shieldLevel;
	public ComponentHealth engineLevel;

	// projectile that the ship will fire
	private GameObject projectile;

	// tracks frames to determine frequency of laser launch
	private float clock;
	// when clock reaches threshold, fires and restarts clock
	private float threshold;


	void Start () {
		laserLevel = GameObject.Find("Lasers").GetComponentInChildren<ComponentHealth>();
		shieldLevel = GameObject.Find("Shields").GetComponentInChildren<ComponentHealth>();
		engineLevel = GameObject.Find("Engines").GetComponentInChildren<ComponentHealth>();

		// loads template for laser prefab instantiation
		projectile = Resources.Load ("Prefabs/PlayerLaser") as GameObject;

		// clock tracks time passed, ship fires when clock passes threshold. clock then resets.
		clock = 0;
		threshold = 30f;
	}
	
	// Update is called once per frame
	void Update () {
		// clock increment is modified by laser health so fire rate is proportional to laser health
		clock += Time.deltaTime*laserLevel.health;
		// fire lasers if thresholds have been reached- extra-fast firing cycle for laser health == 100
		if (laserLevel.health == 100 && clock > threshold/1.5f) {
			Fire ();
		}
		if (clock > threshold) {
			Fire ();
		}

		// move left if "a" is being pressed, right if "d" is being pressed. Confined to LHS.
		if (Input.GetKey ("a") && gameObject.transform.position.x>-6)
			transform.Translate(-(Time.deltaTime * 5 * (engineLevel.health/100)), 0, 0);

		if (Input.GetKey ("d") && gameObject.transform.position.x<-.5)
			transform.Translate(Time.deltaTime * 5 * (engineLevel.health/100), 0, 0);
	}

	// Handles hits
	void OnTriggerEnter2D(Collider2D coll) {
		// different cases for different objects; mostly they just damage the ship
		switch (coll.tag) {
		case "asteroid":
			if (shieldLevel.Damage (30)) {
				Die ();
			}
			break;
		case "Laser":
			if (shieldLevel.Damage (20)) {
				Die ();
			}
			// insert laser consequences and more cases here
			break;
		default:
			break; 
		}
	}

	private void Fire() {
		clock = 0;
		GameObject shot = Instantiate (projectile);
		shot.transform.parent = transform.parent;
		shot.transform.position = new Vector3(transform.position.x, transform.position.y + .75f);
	}

	private void Die() {
		// send some message to the GameController
		Destroy (gameObject);
	}

}
