﻿using UnityEngine;
using System.Collections;

public class Asteroid : ParentEnemy {
	
	private AsteroidModel model;

	public void init(EnemyManager owner) {
		hp = 20;
		fireRate = 0;
		speed = 3;
		this.owner = owner;
		col = gameObject.AddComponent<CircleCollider2D> ();
		body = gameObject.AddComponent<Rigidbody2D> ();
		body.isKinematic = true;
		transform.eulerAngles = new Vector3(0,0,180);
		var modelObject = GameObject.CreatePrimitive (PrimitiveType.Quad);
		model = modelObject.AddComponent<AsteroidModel>();
		gameObject.name = "Asteroid";
		this.tag = "asteroid";
		model.init(this);
	}


	void Update () {
		if (transform.position.y < -7) {
			Destroy (this.gameObject);
		}
		if (hp == 0) {
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
