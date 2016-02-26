using System;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Rocket : MonoBehaviour {

	public RocketModel model;
	private float frame;		// Keep track of time since creation for animation.
	public bool dead;

	public ComponentHealth laserLevel;
	public ComponentHealth engineLevel;
	public ComponentHealth shieldLevel;

	private float healthLoss;

	void Start(){
		healthLoss = 30;
		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the marble texture.
		MeshCollider colid = modelObject.GetComponent<MeshCollider>();
		DestroyImmediate (colid);
		BoxCollider2D circ = modelObject.AddComponent<BoxCollider2D>();
		Rigidbody2D rig = modelObject.AddComponent<Rigidbody2D>();
		model = modelObject.AddComponent<RocketModel>();						// Add a marbleModel script to control visuals of the marble.
		model.init(this);	
		modelObject.SetActive (true);
		rig.mass = 10;
		rig.gravityScale = 0f;
		circ.size = new Vector2(.8f,.8f);
		circ.isTrigger = true;
		circ.enabled = true;
		rig.isKinematic = true;

		frame = 0;

		this.transform.position = new Vector3 (0, -3, 0);
		dead = false;
		laserLevel = GameObject.FindGameObjectWithTag("Laser").GetComponentInChildren<ComponentHealth>();
		shieldLevel = GameObject.FindGameObjectWithTag("Shield").GetComponentInChildren<ComponentHealth>();
		engineLevel = GameObject.FindGameObjectWithTag("Engine").GetComponentInChildren<ComponentHealth>();
	}

	void Update(){
		frame = frame + laserLevel.health/100; //(laserLevel.level/laserLevel.numThresholds);
		if (laserLevel.health == 100 && Mathf.RoundToInt (frame) % 15 == 0) {
			model.reload ();
		}
		if (Mathf.RoundToInt(frame) % 30 == 0) {
			model.reload ();
		}
		float x = transform.position.x;
		float y = transform.position.y;
		if (Input.GetKey ("a") && gameObject.transform.position.x>-6)
			transform.position = new Vector3 (x - Time.deltaTime * 5 * (engineLevel.health/100), y, 0);

		if (Input.GetKey ("d") && gameObject.transform.position.x<-.5)
			transform.position = new Vector3 (x + Time.deltaTime * 5 * (engineLevel.health/100), y, 0);
		
	}

	public void hit() {
		bool half = false;
		if (shieldLevel.health == 100) {
			half = true;
		}
		if ((shieldLevel.health < healthLoss && !half) || shieldLevel.health < healthLoss/2) {
			shieldLevel.health = 0;
			Destroy (model);
			Destroy (gameObject);
		} else {
			if (half) {
				shieldLevel.health -= healthLoss / 2;
			} else {
				shieldLevel.health -= healthLoss;
			}
		}
	}
}

