using UnityEngine;
using System.Collections;

public class SparkEnemy : ParentEnemy {

	private SparkEnemyModel model;
	private bool fired;

	public void init(EnemyManager owner) {
		hp = 5;
		speed = 2;
		col = gameObject.AddComponent<BoxCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		body.isKinematic = true;
		transform.eulerAngles = new Vector3(0,0,180);
		this.owner = owner;
		var modelObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
		model = modelObject.AddComponent<SparkEnemyModel>();	
		model.init(this);
		fired = false;
	}

	void Update () {
		if (hp <= 0) {
			Destroy (this.gameObject);
		}

		Move ();
		float playery = owner.owner.ship.transform.position.y;

		if (playery >= this.transform.position.y) {
			if (!fired) {
				fired = true;
				Fire ("L");
				Fire ("R");
			}
		}

		if (transform.position.y < -7) {
			Destroy (this.gameObject);
		} 
	}

	void Move(){
		transform.Translate (Vector3.up * Time.deltaTime * speed);
	}

	protected void Fire(string direction){  //I made this take x and y because I was thinking about it and different enemies will need to fire from different parts of their models
		GameObject shot1 = new GameObject();
		shot1.transform.parent = transform.parent;
		shot1.transform.position = new Vector3 (0, 0);
		SparkShot leftshot = shot1.AddComponent<SparkShot> ();
		leftshot.GiveDirection (direction);
		shot1.transform.parent = transform.parent;
		shot1.transform.position = new Vector3 (transform.position.x, transform.position.y);
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
