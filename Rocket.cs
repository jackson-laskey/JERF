using System;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Rocket : MonoBehaviour {

	public RocketModel model;

	void Start(){
		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the marble texture.
		MeshCollider colid = modelObject.GetComponent<MeshCollider>();
		DestroyImmediate (colid);
		BoxCollider2D circ = modelObject.AddComponent<BoxCollider2D>();
		Rigidbody2D rig = modelObject.AddComponent<Rigidbody2D>();
		model = modelObject.AddComponent<RocketModel>();						// Add a marbleModel script to control visuals of the marble.
		modelObject.SetActive (true);
		rig.mass = 10;
		rig.gravityScale = 0f;
		circ.size = new Vector2(.8f,.8f);
		circ.isTrigger = true;
		circ.enabled = true;
		rig.isKinematic = true;

		this.transform.position = new Vector3 (0, -3, 0);
		model.init(this);	
	}


}

