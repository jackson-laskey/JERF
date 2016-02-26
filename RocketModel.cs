using UnityEngine;
using System.Collections;

public class RocketModel : MonoBehaviour
{
	private float clock;		// Keep track of time since creation for animation.
	private Rocket owner;			// Pointer to the parent object.
	public Laser laser;
	private Material mat;		// Material for setting/changing texture and color.


	public void init(Rocket owner) {
		this.owner = owner;

		transform.parent = owner.transform;
		transform.localPosition = new Vector3(0,0,0);					// Set the model's parent to the marble.
		name = "Rocket Model";									// Name the object.

		Renderer renderer = GetComponent<Renderer> ();
		mat = GetComponent<Renderer>().material;	// Get the material component of this quad object.
		mat.shader = Shader.Find ("Sprites/Default");						// Tell the renderer that our textures have transparency. 

		renderer.sortingOrder = 1;
		mat.mainTexture = Resources.Load<Texture2D>("Textures/rocket");	// Set the texture.  Must be in Resources folder.
		mat.color = new Color(1,1,1);	
		GameObject laserObject = new GameObject ();
		laser = laserObject.AddComponent<Laser>();
		//laser.init (this.owner);
	}

	void Start () {
		clock = 0f;
	}
		

	public void reload(){
		Destroy (laser.gameObject);
		GameObject laserObject = new GameObject ();
		laser = laserObject.AddComponent<Laser>();
		//laser.init (this.owner);

	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.tag == "asteroid") {
			owner.dead = true;
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D coll){

	}
}



