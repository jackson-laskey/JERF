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

		projectile = Resources.Load ("Prefabs/PlayerLaser") as GameObject;

		clock = 0;
		threshold = 30f;
	}
	
	// Update is called once per frame
	void Update () {
		clock += Time.deltaTime*laserLevel.health;
		if (laserLevel.health == 100 && clock > threshold/2) {
			Fire ();
		}
		if (clock > threshold) {
			Fire ();
		}
		if (Input.GetKey ("a") && gameObject.transform.position.x>-6)
			transform.Translate(-(Time.deltaTime * 5 * (engineLevel.health/100)), 0, 0);

		if (Input.GetKey ("d") && gameObject.transform.position.x<-.5)
			transform.Translate(Time.deltaTime * 5 * (engineLevel.health/100), 0, 0);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		switch (coll.tag) {
		case "asteroid":
			if (shieldLevel.Damage (20)) {
				Die ();
			}
			break;
		case "Laser":
			coll.gameObject.GetComponent<PlayerLaser> ().Hit ();
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
		shot.transform.position = transform.position;
	}

	private void Die() {
		// send some message to the GameController
		Destroy (gameObject);
	}

}
