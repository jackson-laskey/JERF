using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Laser : Projectile {

	public float xScale;
	public float yScale;
	public int pixels;
	private GameObject laser;
	private Animator animator;

	// Use this for initialization
	void Start () {
		//Parameters
		speed = 7;
		xScale = 1f;
		yScale = 1;
		pixels = 200;
		//


		base.init (true, "Laser", xScale, yScale, pixels);
		transform.eulerAngles  = new Vector3(0, 0, 180);
		laser = this.gameObject;
		animator = laser.AddComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Cannon_Projectile_Animation_Controller");
		name = "Laser";
		tag = "Laser";
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "PlayerController") {
			speed = 0;
			animator.SetTrigger ("Hit");
			Destroy (this.gameObject, .2f);
		}
	}
}
