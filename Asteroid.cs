using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Asteroid : MonoBehaviour {

	public AsteroidModel model;
	public AsteroidManager owner;

	public void init(AsteroidManager m){
		owner = m;
		this.transform.parent = owner.transform; 

		var modelObject = GameObject.CreatePrimitive(PrimitiveType.Quad);	// Create a quad object for holding the marble texture.
		MeshCollider colid = modelObject.GetComponent<MeshCollider>();
		DestroyImmediate (colid);
		BoxCollider2D circ = modelObject.AddComponent<BoxCollider2D>();
		Rigidbody2D rig = modelObject.AddComponent<Rigidbody2D>();
		model = modelObject.AddComponent<AsteroidModel>();						// Add a marbleModel script to control visuals of the marble.
		modelObject.SetActive (true);
		rig.mass = 10;
		rig.gravityScale = 0f;
		circ.size = new Vector2(1,1);
		circ.isTrigger = true;
		circ.enabled = true;
		rig.isKinematic = true;

		float x = Random.Range (-10, 9) + Random.value;
		this.transform.position = new Vector3 (x, 6, 0);
		model.init(this);	
		print ("here");
	}


}

