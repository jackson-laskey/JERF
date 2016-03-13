using UnityEngine;
using System.Collections;

public class CannonEnemy : ParentEnemy {

	//private CannonModel model;
	private float stopPosition;
	private string direction;
	private string firingSide;
	public AudioClip LaserSound;
	private float cd;
	private GameObject cannon;
	private GameObject iCannon; //The cannon color. Ryan, this is were you can change the color for the boss
	private Sprite[] cSprites; //Outline: 15, 16  Color: 0, 1
	int oSprite = 15;
	int iSprite = 0;
	private float dmgCount = .3f;


	private float sizex = .65f;
	private float sizey = .65f;

	public void init(EnemyManager owner, float position) {
		hp = 6;
		fireRate = .42f;
		speed = -1.5f;
		stopPosition = position;
		transform.localScale = new Vector3 (sizex, sizey, 1);
		col = gameObject.AddComponent<PolygonCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		SpriteRenderer rend = gameObject.AddComponent<SpriteRenderer> ();
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
		gameObject.name = "CannonEnemy";
		direction = "D";
		firingSide = "L";
		cannon = new GameObject ();
		cannon.transform.parent = this.transform;
		cannon.transform.localPosition = new Vector3 (0, -.36f, 0);
		cannon.transform.localScale = new Vector2 (1, 1);
		cannon.AddComponent<SpriteRenderer> ();
		cSprites = Resources.LoadAll<Sprite> ("Textures/Cannon_Enemy_Sprite_Sheet");
		cannon.GetComponent<SpriteRenderer> ().sprite = cSprites [oSprite];
		cannon.name = "CannonOutline";
		iCannon = new GameObject ();
		iCannon.transform.parent = this.transform;
		iCannon.transform.localPosition = new Vector3 (0, -.36f, 0);
		iCannon.transform.localScale = new Vector2 (1, 1);
		iCannon.AddComponent<SpriteRenderer> ();
		iCannon.GetComponent<SpriteRenderer> ().sprite = cSprites[iSprite];
		iCannon.name = "CannonColor";

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
			
			cannon.GetComponent<SpriteRenderer> ().sprite = cSprites [oSprite];
			iCannon.GetComponent<SpriteRenderer> ().sprite = cSprites[iSprite];
			animator.SetBool ("Moving", false);
			if (cd <= 0) {
				if (firingSide == "L") {
					Fire ((transform.position.x - .45f), (transform.position.y -.36f));
					firingSide = "R";
					cd = fireRate;
				}
				else if (firingSide == "R") {
					Fire ((transform.position.x + .45f), (transform.position.y-.36f));
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
		if (oSprite == 15 && iSprite == 0) {
			oSprite = 16;
			iSprite = 1;
		} else {
			oSprite = 15;
			iSprite = 0;
		}
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
		Destroy (cannon.gameObject);
		Destroy (iCannon.gameObject);
		animator.SetTrigger ("Die");
		Destroy (this.gameObject, 1.1f);
	}
}
