using UnityEngine;
using System.Collections;

public class SparkShot : Projectile {

	public string direction;

	// Use this for initialization
	void Start () {
		speed = 5;
		base.init (true, "Spark", 2f, 3f, 200);
		name = "Spark";
		tag = "Spark";
	}

	// Update is called once per frame
	void Update () {
		if (transform.position.x < -6) {
			Destroy (gameObject);
		}
		if (transform.position.x > 0) {
			Destroy (gameObject);
		}
		Move ();
	}

	public void GiveDirection(string gdirection){
		direction = gdirection;
	}

	void Move(){
		if (direction == "R") {
			transform.Translate (Vector3.right * Time.deltaTime * speed);
		} else if (direction == "L") {
			transform.Translate (Vector3.left * Time.deltaTime * speed);
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "PlayerController") {
			Hit ();
		}
	}
}
