using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuperPlayerLaser : Projectile {
	
		// Use this for initialization
		void Start () {
				speed = 7;
				base.init (false, "SuperPlayerLaser", 2f, 3f, 200);
				name = "SuperPlayerLaser";
			}
	
		// Update is called once per frame
		void Update () {
				base.Update ();
			}
	
		void OnTriggerEnter2D(Collider2D coll) {
			}
	}
