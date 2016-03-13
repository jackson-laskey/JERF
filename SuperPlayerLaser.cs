using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuperPlayerLaser : Projectile {
		
	public float xScale;
	public float yScale;
	public int pixels;

	private GameObject splaser;
	private Animator animator;
		// Use this for initialization
		void Start () {
				//Parameters
				speed = 7;
				xScale = .75f;
				yScale = 1.75f;
		pixels = 200;				
				//
		base.init (false, "SuperPlayerLaser", xScale, yScale, pixels);
				name = "SuperPlayerLaser";
		splaser = this.gameObject;
		animator = splaser.AddComponent<Animator> ();
		animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController> ("Animation/Super_Player_Laser_Animation_Controller");
			}
	
		// Update is called once per frame
		void Update () {
				base.Update ();
			}
	
		void OnTriggerEnter2D(Collider2D coll) {
			}
	}
