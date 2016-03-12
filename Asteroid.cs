using UnityEngine;
using System.Collections;

public class Asteroid : ParentEnemy {
	
	CircleCollider2D collid;


	private float sizex = .65f;
	private float sizey = .65f;
	private float bottomEdge = -7f;


	public void init(EnemyManager owner) {
		hp = 20;
		speed = -2;
		this.owner = owner;
		transform.localScale = new Vector3 (sizex, sizey, 1);
		collid = gameObject.AddComponent<CircleCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		body.isKinematic = true;
		transform.eulerAngles = new Vector3(0,0,0);
		gameObject.name = "Asteroid";
		this.tag = "asteroid";
	}


	void Update () {
		if (transform.position.y < bottomEdge) {
			Destroy (this.gameObject);
		}
		if (hp <= 0) {
			Destroy (this.gameObject);
		}
		Move ();
	}

	void Move(){
		transform.Translate (Vector3.up * Time.deltaTime * speed);
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
