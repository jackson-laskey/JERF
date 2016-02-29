using UnityEngine;
using System.Collections;

public class SmallEnemy : ParentEnemy {

	private SmallEnemyModel model;
	private string direction;
	private int diveSpeed;
	private int divePosition;
	private bool diving;

	public void init(EnemyManager owner) {
		hp = 1;
		fireRate = 0;
		speed = 1;
		diveSpeed = 10;
		divePosition = -1;
		col = gameObject.AddComponent<BoxCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		body.isKinematic = true;
		transform.eulerAngles = new Vector3(0,0,180);
		this.owner = owner;
		direction = "L";
		var modelObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
		model = modelObject.AddComponent<SmallEnemyModel>();	
		model.init(this);
		diving = false;
	}
		
	void Update () {
		if (hp == 0) {
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
				float playerx = owner.owner.ship.transform.GetChild (0).position.x;
				float playery = owner.owner.ship.transform.GetChild (0).position.y;
				float angle = Mathf.Rad2Deg*Mathf.Acos (Mathf.Abs (playery - this.transform.position.y) / Mathf.Sqrt (Mathf.Pow((playerx - this.transform.position.x),2) + Mathf.Pow((playery - this.transform.position.y),2)));
				float sign = (playerx - this.transform.position.x) / Mathf.Abs (playerx - this.transform.position.x);
				transform.eulerAngles = new Vector3 (0, 0, 180 + (sign*angle));
				diving = true;
			}
		} else {
			Dive ();
		}
		if (transform.position.y < -7) {
			Destroy (this.gameObject);
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
		if (other.tag == "PlayerLaser") {
			hp--;
		}
		if (other.tag == "PlayerController") {
			Destroy (this.gameObject);
		}
	}
}
