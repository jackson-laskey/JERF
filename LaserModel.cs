using UnityEngine;
using System.Collections;


public class LaserModel : MonoBehaviour
{
	private float clock;		// Keep track of time since creation for animation.
	private LaserMS owner;			// Pointer to the parent object.
	private Material mat;		// Material for setting/changing texture and color.
	private float xPerm;


	public void init(LaserMS owner) {
		this.owner = owner;

		transform.parent = owner.transform;		// Center the model on the parent.
		transform.localPosition = new Vector3(0,0,0);
		transform.localScale = new Vector3 (1, 1, 1);
		name = "Laser Model";									// Name the object.

		Renderer renderer = GetComponent<Renderer> ();
		mat = GetComponent<Renderer>().material;	// Get the material component of this quad object.
		mat.shader = Shader.Find ("Sprites/Default");						// Tell the renderer that our textures have transparency. 

		renderer.sortingOrder = 1;
		mat.mainTexture = Resources.Load<Texture2D>("Textures/laser");	// Set the texture.  Must be in Resources folder.
		mat.color = new Color(1,1,1);											// Set the color (easy way to tint things).
		xPerm = transform.position.x;
	}

	void Start () {
		clock = 0f;
	}

	void Update () {
		//float x = transform.position.x;
		float y = transform.position.y;
		transform.position = new Vector3 (xPerm, y + Time.deltaTime * 20f, 0);
		if(y>5){
			//owner.done = true;
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "asteroid") {
			owner.done = true;
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll){

	}

}