using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuperPlayerLaser : Projectile {
		
	public float xScale;
	public float yScale;
	public int pixels;
		// Use this for initialization
		void Start () {
				//Parameters
				speed = 7;
				xScale = 2f;
				yScale = 3f;
		pixels = 200;				
				//
		base.init (false, "SuperPlayerLaser", xScale, yScale, pixels);
				name = "SuperPlayerLaser";
			}
	
		// Update is called once per frame
		void Update () {
				base.Update ();
			}
	
		void OnTriggerEnter2D(Collider2D coll) {
			}
	}
