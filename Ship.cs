using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {

	// the health bar of each component on the RHS; 0 <= .health <= 100
	public ComponentHealth laserLevel;
	public ComponentHealth shieldLevel;
	public ComponentHealth engineLevel;
	public GameObject death;
	public GameObject Jet;
	private Animator animator;
	private Animator jets;

	// projectile that the ship will fire
	private GameObject projectile;

	// tracks frames to determine frequency of laser launch
	private float clock;
	// when clock reaches threshold, fires and restarts clock
	private float fireInterval;


	void Start () {
		laserLevel = GameObject.Find("Lasers").GetComponentInChildren<ComponentHealth>();
		shieldLevel = GameObject.Find("Shields").GetComponentInChildren<ComponentHealth>();
		engineLevel = GameObject.Find("Engines").GetComponentInChildren<ComponentHealth>();
		animator = this.GetComponent<Animator> ();
		animator.SetInteger ("Direction", 0);
		jets = Jet.GetComponent<Animator> ();
		jets.SetInteger ("Direction", 0);
		jets.SetInteger ("Power", 3);

		// loads template for laser prefab instantiation
		projectile = Resources.Load ("Prefabs/PlayerLaser") as GameObject;

		// clock tracks time passed, ship fires when clock passes threshold. clock then resets.
		clock = 0;
		fireInterval = 32f;
	}
	
	// Update is called once per frame
	void Update () {
		// clock increment is modified by laser health so fire rate is proportional to laser health
		clock += Time.deltaTime*laserLevel.health;
		// fire lasers if thresholds have been reached- extra-fast firing cycle for laser health == 100
		if (laserLevel.health == 100 && clock > fireInterval/1.3f) {
			Fire ();
		}
		if (clock > fireInterval) {
			Fire ();
		}

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

	private void Fire() {
		clock = 0;
		GameObject shot = new GameObject();
		shot.transform.parent = transform.parent;
		shot.transform.position = new Vector3 (0, 0);
		shot.AddComponent<PlayerLaser> ();
		shot.transform.parent = transform.parent;
		shot.transform.position = new Vector3(transform.position.x, transform.position.y + .75f);
	}

	private void Die() {
		// send some message to the GameController
		var x = Instantiate(death, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
			Destroy (gameObject);
	}

}
