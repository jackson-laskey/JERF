using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Laser : Projectile {

	// Use this for initialization
	void Start () {
		speed = 7;
		name = "Laser";
		gameObject.transform.localScale = new Vector3 (.2f, .8f, 1);
		base.init (true);
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
