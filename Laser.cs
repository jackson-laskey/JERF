using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Laser : Projectile {

	// Use this for initialization
	void Start () {
		speed = 7;
		base.init (true, "Laser", 2f, 1f, 200);
		name = "Laser";
		tag = "Laser";
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "PlayerController") {
			Hit ();
		}
	}
}
