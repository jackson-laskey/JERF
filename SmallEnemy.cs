using UnityEngine;
using System.Collections;

public class SmallEnemy : ParentEnemy {

	private SmallEnemyModel model;
	private string direction;
	private bool diving;



	private float diveSpeed = 5;
	private float divePosition = .5f;
	private float size = .85f;
	private float bottomEdge = -7f;


	public void init(EnemyManager owner) {
		name = "SmallEnemy";
		hp = 2;
		speed = .8f;
		transform.localScale = new Vector3 (size, 1f, 1f);
		col = gameObject.AddComponent<BoxCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		body.isKinematic = true;
		transform.eulerAngles = new Vector3(0,0,180);
		this.owner = owner;
		if (transform.position.x > -3f) { 
			direction = "L";
		} else {
			direction = "R";
		}
		var modelObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
		model = modelObject.AddComponent<SmallEnemyModel>();	
		model.init(this);
		diving = false;
	}
		
	void Update () {
		if (hp <= 0 || transform.position.y < bottomEdge) {
			Destroy (this.gameObject);
		}
		if (!diving) {
			if (transform.position.x <= -6) {
				direction = "R";
			}
			else if (transform.position.x >= 0) {
				direction = "L";
			}
			Move ();
			if (transform.position.y <= divePosition) {
 				float playerx = owner.owner.ship.transform.position.x;
				float playery = owner.owner.ship.transform.position.y;
				float angle = Mathf.Rad2Deg*Mathf.Acos (Mathf.Abs (playery - this.transform.position.y) / Mathf.Sqrt (Mathf.Pow((playerx - this.transform.position.x),2) + 
					Mathf.Pow((playery - this.transform.position.y),2)));
				float sign = (playerx - this.transform.position.x) / Mathf.Abs (playerx - this.transform.position.x);
				transform.eulerAngles = new Vector3 (0, 0, 180 + (sign*angle));
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
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "PlayerLaser" || other.name == "SuperPlayerLaser") {
			hp--;
		}
		if (other.tag == "PlayerController") {
			Destroy (this.gameObject);
		}
	}
}
