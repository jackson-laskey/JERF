using System;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Rocket : MonoBehaviour {

	public RocketModel model;
	private int frame;		// Keep track of time since creation for animation.
	public bool dead;

	void Start(){
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
	}

	void Update(){
		if (dead) {
			Destroy (gameObject);
		}

		frame = frame + 1;
		if (frame % 30 == 0) {
			model.reload ();
		}
		float x = transform.position.x;
		float y = transform.position.y;
		if (Input.GetKey (KeyCode.LeftArrow))
			transform.position = new Vector3 (x - Time.deltaTime * 5, y, 0);

		if (Input.GetKey (KeyCode.RightArrow))
			transform.position = new Vector3 (x + Time.deltaTime * 5, y, 0);
		
	}
}

