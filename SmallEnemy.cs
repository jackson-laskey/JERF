using UnityEngine;
using System.Collections;

public class SmallEnemy : ParentEnemy {

	private SmallEnemyModel model;
	private string direction;
	private int diveSpeed;
	private int divePosition;
	private bool diving;

	void init(EnemyManager owner) {
		hp = 1;
		fireRate = 0;
		speed = 4;
		diveSpeed = 6;
		divePosition = -1;
		col = new Collider2D();
		body = new Rigidbody2D();
		transform.eulerAngles = new Vector3(0,0,180);
		this.owner = owner;
		direction = "L";
		var modelObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
		model = modelObject.AddComponent<SmallEnemyModel>();	
		model.init(this);
	}
		
	void Update () {
		if (!diving) {
			Move ();
			if (transform.position.y <= divePosition) {
				int playerx = owner.owner.ship.transform.GetChild (0).position.x;
				int playery = owner.owner.ship.transform.GetChild (0).position.y;
				float angle = Mathf.Acos (Mathf.Abs (playery - this.transform.position.y) / Mathf.Sqrt ((playerx - this.transform.position.x) ^ 2 + (playery - this.transform.position.y) ^ 2));
				transform.eulerAngles = new Vector3 (0, 0, 180 - angle);
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
			transform.Translate (Vector3.right * Time.deltaTime * speed);
		} else {
			transform.Translate (Vector3.up * Time.deltaTime * speed);
			transform.Translate (Vector3.right * Time.deltaTime * speed);
		}
	}

	void Dive(){
		transform.Translate (Vector3.down * Time.deltaTime * diveSpeed);
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
