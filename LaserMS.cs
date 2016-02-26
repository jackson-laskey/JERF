using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LaserMS : MonoBehaviour {
	
	public Rocket rocket;
	public LaserModel model;
	public bool done;

	public void init(Rocket r){
		rocket = r;
		this.transform.position = new Vector3 (rocket.transform.position.x, rocket.transform.position.y, 0);
		transform.parent = rocket.transform;
		transform.localScale -= new Vector3(.8f,.5f,.9f);


		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the marble texture.
		MeshCollider colid = modelObject.GetComponent<MeshCollider>();
		DestroyImmediate (colid);
		BoxCollider2D circ = modelObject.AddComponent<BoxCollider2D>();
		Rigidbody2D rig = modelObject.AddComponent<Rigidbody2D>();
		model = modelObject.AddComponent<LaserModel>();						// Add a marbleModel script to control visuals of the marble.
		modelObject.SetActive (true);
		rig.mass = 10;
		rig.gravityScale = 0f;
		circ.size = new Vector2(1,1);
		circ.isTrigger = true;
		circ.enabled = true;
		rig.isKinematic = true;
		done = false;
		//model.init (this);
	}



}

