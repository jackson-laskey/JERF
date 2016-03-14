using UnityEngine;
using System.Collections;

public class CannonEnemy : ParentEnemy {

	//private CannonModel model;
	private float stopPosition;
	private string direction;
	private string firingSide;
	public AudioClip LaserSound;
	public AudioClip burst;
	private float cd;
	private GameObject Lcannon;
	private GameObject LiCannon; 
	private GameObject Rcannon;
	private GameObject RiCannon;
	private Sprite[] cSprites;
	private float dmgCount = .3f;
	public bool dead = false;

	private float sizex = .65f;
	private float sizey = .65f;

	public void init(EnemyManager owner, float position) {
		hp = 6;
		fireRate = .42f;
		speed = -1.5f;
		stopPosition = position;
		transform.localScale = new Vector3 (sizex, sizey, 1);

		SpriteRenderer rend = gameObject.AddComponent<SpriteRenderer> ();
		cSprites = Resources.LoadAll<Sprite> ("Textures/Cannon_Enemy_Sprite_Sheet");
		rend.sprite = cSprites[12];
		col = gameObject.AddComponent<PolygonCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		this.GetComponent<Renderer> ().sortingOrder = 1;
		animator = gameObject.AddComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/CE_Animation_Controller");
		animator.SetFloat ("MoveRate", .8f);
		animator.SetBool ("Moving", true);
		animator.SetBool ("Damaged", false);
		body.isKinematic = true;
		transform.localScale = new Vector3 (1.5f, 1.5f, 0);
		this.owner = owner;
		LaserSound = Resources.Load ("Sounds/laser") as AudioClip;
		burst = Resources.Load ("Sounds/burst") as AudioClip;

		gameObject.name = "CannonEnemy";
		direction = "D";
		firingSide = "L";
		rend.sortingOrder = 2;
		cSprites = Resources.LoadAll<Sprite> ("Textures/Cannons_Sprite_Sheet");
		Lcannon = new GameObject ();
		Lcannon.transform.parent = this.transform;
		Lcannon.transform.localPosition = new Vector3 (.305f, -.364f, 0);
		Lcannon.transform.localScale = new Vector2 (1, 1);
		Lcannon.AddComponent<SpriteRenderer> ();
		Lcannon.GetComponent<SpriteRenderer> ().sprite = cSprites [0];
		Lcannon.name = "LeftCannonOutline";
		LiCannon = new GameObject ();
		LiCannon.transform.parent = Lcannon.transform;
		LiCannon.transform.localPosition = new Vector3 (0, 0, 0);
		LiCannon.transform.localScale = new Vector2 (1, 1);
		LiCannon.AddComponent<SpriteRenderer> ();
		LiCannon.GetComponent<SpriteRenderer> ().sprite = cSprites[1];
		LiCannon.name = "LeftCannonColor";
		Rcannon = new GameObject ();
		Rcannon.transform.parent = this.transform;
		Rcannon.transform.localPosition = new Vector3 (-.305f, -.364f, 0);
		Rcannon.transform.localScale = new Vector2 (1, 1);
		Rcannon.AddComponent<SpriteRenderer> ();
		Rcannon.GetComponent<SpriteRenderer> ().sprite = cSprites [0];
		Rcannon.name = "RightCannonOutline";
		RiCannon = new GameObject ();
		RiCannon.transform.parent = Rcannon.transform;
		RiCannon.transform.localPosition = new Vector3 (0, 0, 0);
		RiCannon.transform.localScale = new Vector2 (1, 1);
		RiCannon.AddComponent<SpriteRenderer> ();
		RiCannon.GetComponent<SpriteRenderer> ().sprite = cSprites[1];
		RiCannon.name = "RightCannonColor";

		Lcannon.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		LiCannon.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		Rcannon.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		RiCannon.GetComponent<SpriteRenderer> ().sortingOrder = 1;

	}
	
	// Update is called once per frame
	void Update () {

		if (hp <= 0) {
			animator.SetBool ("Damaged", false);
			speed = -.2f;
			Die ();
		}
		if (animator.GetBool ("Damaged")) {
			dmgCount = dmgCount - Time.deltaTime;
		}
		if (dmgCount <= 0) {
			animator.SetBool ("Damaged", false);
			dmgCount = .3f;
		}

		if (transform.position.y < stopPosition) {
			direction = "L";	
			transform.position = new Vector3(transform.position.x,stopPosition,0);

		}
		if (transform.position.x <= -6) {
			direction = "R";
		}
		if (transform.position.x >= -1) {
			direction = "L";
		}
		Move ();


		if (transform.position.y == stopPosition) {
			animator.SetBool ("Moving", false);
			if (cd <= 0) {
				if (firingSide == "L") {
					Fire ((transform.position.x - .45f), (transform.position.y -.55f));
					Lcannon.transform.localPosition = new Vector3 (.305f, -.33f, 0);
					Rcannon.transform.localPosition = new Vector3 (-.305f, -.364f, 0);
					firingSide = "R";
					cd = fireRate;
				}
				else if (firingSide == "R") {
					Fire ((transform.position.x + .45f), (transform.position.y-.55f));
					Rcannon.transform.localPosition = new Vector3 (-.305f, -.33f, 0);
					Lcannon.transform.localPosition = new Vector3 (.305f, -.364f, 0);
					firingSide = "L";
					cd = fireRate;
				}
			} else {
				cd = cd - Time.deltaTime;
			}
		}

	}

	void Move(){
		if (direction == "D") {
			transform.Translate (Vector3.up * Time.deltaTime * speed);
		} else if (direction == "L") {
			transform.Translate (Vector3.right * Time.deltaTime * speed);
		} else {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		}
	}

	//I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
	protected void Fire(float x, float y){ 	
		AudioSource.PlayClipAtPoint(LaserSound, transform.position);
		GameObject shot = new GameObject();
		shot.transform.parent = transform.parent;
		shot.transform.position = new Vector3 (0, 0);
		shot.AddComponent<Laser> ();
		shot.transform.parent = transform.parent;
		shot.transform.position = new Vector3 (x, y);
	}

    void OnTriggerEnter2D(Collider2D other){
		if (other.name == "PlayerLaser") {
			hp--;
			animator.SetBool ("Damaged", true);
		}
		if (other.name == "SuperPlayerLaser") {
			hp--;
			animator.SetBool ("Damaged", true);
		}
	}

	void Die(){
		if (!dead) {
			AudioSource.PlayClipAtPoint (burst, transform.position);
			dead = true;
		}
		Destroy (Rcannon.gameObject);
		Destroy (RiCannon.gameObject);
		Destroy (Lcannon.gameObject);
		Destroy (LiCannon.gameObject);
		animator.SetTrigger ("Die");
		Destroy (this.gameObject, 1.1f);
	}
}
