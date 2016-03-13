using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Ship : MonoBehaviour {

	// the health bar of each component on the RHS; 0 <= .health <= 100
	public LaserHealth laserLevel;
	public ComponentHealth shieldLevel;
	public ComponentHealth engineLevel;
	public GameObject death;
	private Animator direction;
	public GameObject JET;
	private Animator jets;
	private Button restart;
	public AudioClip EngineSound;
	public AudioClip ShieldSound;
	public AudioClip LaserSound;
	public AudioClip DoubleLaserSound;
	public AudioClip SuperLaserSound;
	public AudioClip CollisionSound;
	public AudioClip DeathSound;

	public AudioSource audio;




	// reference to GameController script
	public GameController controller;

	// tracks frames to determine frequency of laser launch
	private float clock;
	// when clock reaches threshold, fires and restarts clock
	private float fireInterval = .4f;

	private bool movingTwoD;
	private float fallBackSpeed = 2.5f;
	float speedRatioTwoD = .7f;


	private bool initd = false;

	private float speed;

	//Engine Health threshholds
	public int EHT1 = 99;
	public int EHT2 = 70;
	public int EHT3 = 30;
	public int EHT4 = 1;

	//Engine Speed threshholds
	public int EST1 = 100;
	public int EST2 = 85;
	public int EST3 = 50;
	public int EST4 = 20;
	public int EST5 = 0;

	//Laser Threshholds
	public int LT1 = 100;
	public int LT2 = 50;

	//Damages
	public int ADamage = 49;
	public int LDamage = 15;
	public int SEDamage = 30;
	public int BDamage = 30;
	public int SPDamage = 50;




	// DAMAGE TAKEN HANDLED IN ONTRIGGER2DENTER()
	// ENGINE THRESHOLDS AND SHIELD COLOR THRESHOLDS HANDLED IN INIT


	public void init (GameController gContr, Button r) {
		controller = gContr;
		gameObject.AddComponent<Rigidbody2D> ().isKinematic = true;
		gameObject.AddComponent<PolygonCollider2D> ().isTrigger = true;
		transform.localPosition = new Vector3 (-3.2f, -4, 0);
		gameObject.name = "Ship";
		tag = "PlayerController";


		restart = r;

		//laserLevel = GameObject.Find("Lasers").GetComponentInChildren<ComponentHealth>();

		laserLevel = GameObject.Find("Lasers").GetComponentInChildren<LaserHealth>();
		shieldLevel = GameObject.Find("Shields").GetComponentInChildren<ComponentHealth>();
		engineLevel = GameObject.Find("Engines").GetComponentInChildren<ComponentHealth>();
		JET = GameObject.Find ("Jets");
		direction = this.gameObject.GetComponent<Animator> ();
		jets = JET.GetComponent<Animator> ();

		direction.SetInteger ("Direction", 0);
		jets.SetInteger ("Power", 3);
		// loads template for laser prefab instantiation


		// clock tracks time passed, ship fires when clock passes threshold. clock then resets.
		clock = 0;

		movingTwoD = false;

		initd = true;

		EngineSound = Resources.Load ("Sounds/EngineSound") as AudioClip;
		LaserSound = Resources.Load ("Sounds/laser") as AudioClip;
		DoubleLaserSound = Resources.Load ("Sounds/doubleLaser") as AudioClip;
		SuperLaserSound = Resources.Load ("Sounds/superLaser") as AudioClip;		
		CollisionSound = Resources.Load ("Sounds/collision") as AudioClip;
		DeathSound = Resources.Load ("Sounds/death") as AudioClip;
		audio = gameObject.AddComponent<AudioSource> ();
		audio.loop = true;
		audio.clip = EngineSound;
		audio.Play();

		ShieldSound = Resources.Load ("Sounds/shields") as AudioClip;
		shieldLevel.audio.clip = ShieldSound;
		shieldLevel.audio.loop = true;
		shieldLevel.audio.Play ();

	}
	
//	 Update is called once per frame
	void Update () {

		//Sound Stuff
		audio.volume = (engineLevel.health + 1)/200;
		shieldLevel.audio.volume = (shieldLevel.health + 1) / 200;
		//

		if (!initd) {
			return;
		}

		//
		// THRESHOLDS HANDLED BELOW
		//
		if (engineLevel.health > EHT1) {
			speed = EST1;
			movingTwoD = true;
		} else if (engineLevel.health > EHT2) {
			speed = EST2;
			movingTwoD = false;
		} else if (engineLevel.health > EHT3) {
			speed = EST3;
		} else if (engineLevel.health > EHT4) {
			speed = EST4;
		} else {
			speed = EST5;
		}
		// clock increment is modified by laser health so fire rate is proportional to laser health
		clock += Time.deltaTime;
		// fire lasers if thresholds have been reached- extra-fast firing cycle for laser health == 100
		if (clock > fireInterval && Input.GetKey(KeyCode.Space) && laserLevel.health>0){
			Fire ();
		}

		if (engineLevel.health >= EHT1) {
			jets.SetInteger ("Power", 4);
		} if (engineLevel.health < EHT1) {
			jets.SetInteger ("Power", 3);
			JET.SetActive (true);
		} if (engineLevel.health <= EHT2) {
			jets.SetInteger ("Power", 2);
			JET.SetActive (true);
		} if (engineLevel.health <= EHT3) {
			jets.SetInteger ("Power", 1);
			JET.SetActive (true);
		} if (engineLevel.health == 0) {
			JET.SetActive (false);
		}

		if (transform.position.y > -4 && !movingTwoD) {
			transform.Translate (0, -fallBackSpeed * Time.deltaTime, 0);
		}

		// move left if "a" is being pressed, right if "d" is being pressed. Confined to LHS.
		if (movingTwoD) {
			if (Input.GetKey ("s") && gameObject.transform.position.y >= -4f) {
				transform.Translate (0, -speedRatioTwoD * 5 * Time.deltaTime, 0);
			} else if (Input.GetKey ("w") && gameObject.transform.position.y <= 2f) {
				transform.Translate (0, speedRatioTwoD * 5 * Time.deltaTime, 0);
			}
		}
		if (Input.GetKey ("a") && gameObject.transform.position.x > -6) {
			jets.SetInteger ("Direction", 1);
			direction.SetInteger ("Direction", 1);
			JET.transform.localPosition = new Vector3 (.02f, -.37f, 0);
			transform.Translate (-(Time.deltaTime * 5 * (speed / 100)), 0, 0);

		} else if (Input.GetKey ("d") && gameObject.transform.position.x < -.5) {
			jets.SetInteger ("Direction", 2);
			direction.SetInteger ("Direction", 2);
			JET.transform.localPosition = new Vector3 (-.02f, -.37f, 0);
			transform.Translate (Time.deltaTime * 5 * (speed / 100), 0, 0);
		} else {
			jets.SetInteger ("Direction", 0);
			JET.transform.localPosition = new Vector3 (0, -.38f, 0);
			direction.SetInteger ("Direction", 0);
        }
	}

	// Handles hits
	void OnTriggerEnter2D(Collider2D coll) {
		// different cases for different objects; mostly they just damage the ship
		switch (coll.name) {
		case "Asteroid":
			AudioSource.PlayClipAtPoint(CollisionSound,this.transform.position);

			if (shieldLevel.Damage (ADamage)) {
				Die ();
			}
			break;
		case "Laser":
			if (shieldLevel.Damage (LDamage)) {
				Die ();
			}
			break;
		case "SmallEnemy":
			AudioSource.PlayClipAtPoint(CollisionSound,this.transform.position);

			if (shieldLevel.Damage (SEDamage)) {
				Die ();
			}
			break;
		case "Spark":
			if (shieldLevel.Damage (SPDamage)) {
				Die ();
			}
			break;
		case "BeamEnemy":
			if (shieldLevel.Damage (BDamage)) {
				Die ();
			}
			break;
		case "P1":
			shieldLevel.PowerUp ();
			break;
		case "P2":
			engineLevel.PowerUp ();
			break;
		case "P3":
			laserLevel.PowerUp ();
			break;
		default:
			break; 
		}
	}

	private void Fire() {
				if (laserLevel.health < LT2) {
			AudioSource.PlayClipAtPoint(LaserSound,this.transform.position);

						GameObject shot = new GameObject ();
						shot.transform.parent = transform.parent;
						shot.AddComponent<PlayerLaser> ();
						shot.transform.position = new Vector3 (transform.position.x, transform.position.y + .7f);
				} else if (laserLevel.health < LT1) {
			AudioSource.PlayClipAtPoint(DoubleLaserSound,this.transform.position);
						GameObject shot = new GameObject ();
						shot.transform.parent = transform.parent;
						shot.AddComponent<PlayerLaser> ();
						shot.transform.position = new Vector3 (transform.position.x-.1f, transform.position.y + .7f);
						GameObject shot2 = new GameObject ();
						shot2.transform.parent = transform.parent;
						shot2.AddComponent<PlayerLaser> ();
						shot2.transform.position = new Vector3 (transform.position.x+.1f, transform.position.y + .7f);
				} else {
			AudioSource.PlayClipAtPoint(SuperLaserSound,this.transform.position);
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
		clock = 0;
	}

	private void Die() {
		// send some message to the GameController
		audio.Pause();
		shieldLevel.audio.Pause ();
		GameObject death = new GameObject ();
		controller.MakeSprite (death,"", GameObject.Find("ShipHandler").transform, 0, 0, 1, 1, 500);
		death.transform.localPosition = this.transform.position;
		death.AddComponent<Animator> ();
		Animator animator = death.GetComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/ship_Death_Controller");
		death.transform.localScale = new Vector3 (2, 2, 0);
		death.name = "Death";
		controller.GetComponentInChildren<CaptainManager> ().Die ();
		restart.gameObject.SetActive (true);
		Destroy (this.gameObject);
	}

}
