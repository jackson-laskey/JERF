using UnityEngine;
using System.Collections;

public class Asteroid : ParentEnemy {
	
	private AsteroidModel model;


	private float sizex = .65f;
	private float sizey = .65f;
	private float bottomEdge = -7f;


	public void init(EnemyManager owner) {
		hp = 20;
		speed = -2;
		this.owner = owner;
		transform.localScale = new Vector3 (Random.Range(sizex,sizex+.25f), Random.Range(sizey,sizey+.25f), 1);
		col = gameObject.AddComponent<PolygonCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		body.isKinematic = true;
		transform.eulerAngles = new Vector3(0,0,0);
		var modelObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
		model = modelObject.AddComponent<AsteroidModel>();
		gameObject.name = "Asteroid";
		this.tag = "asteroid";
		model.init(this);
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
