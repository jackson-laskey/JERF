using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLaser : Projectile {

	// Use this for initialization
	void Start () {
		speed = 7;
		base.init (false, "PlayerLaser", 2f, 2f, 200);
		name = "PlayerLaser";
		tag = "PlayerLaser";
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.name != "Ship") {
			Hit ();
		}
	}
}

