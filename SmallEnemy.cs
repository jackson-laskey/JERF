using UnityEngine;
using System.Collections;

public class SmallEnemy : ParentEnemy {


	private string direction;
	private bool diving;



	private float diveSpeed = -5;
	private float divePosition = 1f;
	private float sizex = .65f;
	private float sizey = .65f;
	private float bottomEdge = -7f;
	private float dmgCount = .3f;


	public void init(EnemyManager owner) {
		name = "SmallEnemy";
		hp = 2;
		speed = -1f;
		transform.localScale = new Vector3 (sizex, sizey, 1);
		SpriteRenderer rend = this.gameObject.AddComponent<SpriteRenderer> ();
		col = gameObject.AddComponent<BoxCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		animator = gameObject.AddComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/SE_Animation_Controller");
		animator.SetFloat ("Charging", 1.0f);
		animator.SetBool ("Damaged", false);
		animator.SetBool ("Charged", false);
		body.isKinematic = true;
		transform.eulerAngles = new Vector3(0,0,0);
		transform.localScale = new Vector3 (1.25f, 1.25f, 0);
		this.owner = owner;
		if (transform.position.x > -3f) { 
			direction = "L";
		} else {
			direction = "R";
		}

		diving = false;
	}
		
	void Update () {
		
		if (hp <= 0) {
			speed = 0;
			animator.SetBool ("Damaged", false);
			Die ();
		}
		if (animator.GetBool ("Damaged")) {
			dmgCount = dmgCount - Time.deltaTime;
		}
		if (dmgCount <= 0) {
			animator.SetBool ("Damaged", false);
			dmgCount = .3f;
		}
		if ( transform.position.y < bottomEdge){
			Destroy(this.gameObject);
		}
		if (!diving) {
			if (transform.position.x <= -6) {
				direction = "R";
			}
			else if (transform.position.x >= -1) {
				direction = "L";
			}
			Move ();
			if (transform.position.y <= divePosition) {
 				float playerx = owner.owner.ship.transform.position.x;
				float playery = owner.owner.ship.transform.position.y;
				float angle = Mathf.Rad2Deg*Mathf.Acos (Mathf.Abs (playery - this.transform.position.y) / Mathf.Sqrt (Mathf.Pow((playerx - this.transform.position.x),2) + 
					Mathf.Pow((playery - this.transform.position.y),2)));
				float sign = (playerx - this.transform.position.x) / Mathf.Abs (playerx - this.transform.position.x);
				transform.eulerAngles = new Vector3 (0, 0, 0 + (sign*angle));
				diving = true;
			}
		} else {
			Dive ();
		}
	}

	void Move(){
		if (direction == "L") {
			transform.Translate (Vector3.up * Time.deltaTime * speed);
			transform.Translate (Vector3.right * Time.deltaTime * speed*3);
		} else {
			transform.Translate (Vector3.up * Time.deltaTime * speed);
			transform.Translate (Vector3.left * Time.deltaTime * speed*3);
		}
	}

	void Dive(){
		transform.Translate (Vector3.up * Time.deltaTime * diveSpeed);
		animator.SetFloat ("Charging", 2);

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "PlayerLaser" || other.name == "SuperPlayerLaser") {
			hp--;
			animator.SetBool ("Damaged", true);
		}
		if (other.tag == "PlayerController") {
			animator.SetBool ("Charged", true);
			Die ();
		}
	}

	void Die(){
		speed = 0;
		diveSpeed = 0;
		animator.SetTrigger ("Die");
		if (animator.GetBool ("Charged")) {
			Destroy (this.gameObject, .4f);
		} else {
			Destroy (this.gameObject, .8f);
		}

	}
}
