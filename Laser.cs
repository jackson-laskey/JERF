using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Laser : Projectile {

	public float xScale;
	public float yScale;
	public int pixels;

	// Use this for initialization
	void Start () {
		//Parameters
		speed = 7;
		xScale = 2f;
		yScale = 1;
		pixels = 200;
		//


		base.init (true, "Laser", xScale, yScale, pixels);
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
