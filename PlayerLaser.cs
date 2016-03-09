﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLaser : Projectile {

	public float xScale;
	public float yScale;
	public int pixels;

	// Use this for initialization
	void Start () {
		//PARAMETERS
		speed = 7;
		xScale = 2f;
		yScale = 2f;
		pixels = 200;
		//

		base.init (false, "PlayerLaser",  xScale, yScale, pixels);
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

