using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLaser : Projectile {

	public float xScale;
	public float yScale;
	public int pixels;
	private GameObject laser;
	private Animator animator;

	// Use this for initialization
	void Start () {
		//PARAMETERS
		speed = 7;
		xScale = 1f;
		yScale = 1.25f;
		pixels = 200;
		//

		base.init (false, "PlayerLaser",  xScale, yScale, pixels);
		laser = this.gameObject;
		animator = laser.AddComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Player_Laser_Animation_Controller");
		name = "PlayerLaser";
		tag = "PlayerLaser";
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		coll.isTrigger = false;
		if (coll.name == "Asteroid") {
			animator.SetTrigger ("Destroyed");
			speed = 0;
			Destroy (this.gameObject, .3f);
		}
		if (coll.name != "Ship" && coll.name != "Beam" && coll.name != "Dead" && coll.name != "P1" && coll.name != "P2" && coll.name != "P3") {
			animator.SetTrigger ("Hit");
			speed = 0;
			Destroy (this.gameObject, .2f);
		}
	}
}

