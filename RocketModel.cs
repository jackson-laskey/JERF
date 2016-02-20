using UnityEngine;
using System.Collections;

public class RocketModel : MonoBehaviour
{
	private float clock;		// Keep track of time since creation for animation.
	private Rocket owner;			// Pointer to the parent object.
	private Material mat;		// Material for setting/changing texture and color.


	public void init(Rocket owner) {
		this.owner = owner;

		transform.parent = owner.transform;					// Set the model's parent to the marble.
		transform.localPosition = new Vector3(0,0,0);		// Center the model on the parent.
		name = "Rocket Model";									// Name the object.

		Renderer renderer = GetComponent<Renderer> ();
		mat = GetComponent<Renderer>().material;	// Get the material component of this quad object.
		mat.shader = Shader.Find ("Sprites/Default");						// Tell the renderer that our textures have transparency. 

		renderer.sortingOrder = 1;
		mat.mainTexture = Resources.Load<Texture2D>("Textures/rocket");	// Set the texture.  Must be in Resources folder.
		mat.color = new Color(1,1,1);											// Set the color (easy way to tint things).
	}

	void Start () {
		clock = 0f;
	}

	void Update () {
		float x = transform.position.x;
		float y = transform.position.y;
		if (Input.GetKey (KeyCode.LeftArrow))
			transform.position = new Vector3 (x - Time.deltaTime * 5, y, 0);

		if (Input.GetKey (KeyCode.RightArrow))
			transform.position = new Vector3 (x + Time.deltaTime * 5, y, 0);
	}

	void OnTriggerEnter2D(Collider2D coll){
		Destroy (gameObject);
	}

	void OnCollisionEnter2D(Collision2D coll){

	}
}



