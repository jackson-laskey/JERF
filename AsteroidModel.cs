using UnityEngine;
using System.Collections;

public class AsteroidModel : MonoBehaviour
{
	private float clock;		// Keep track of time since creation for animation.
	private Asteroid owner;			// Pointer to the parent object.
	private Material mat;		// Material for setting/changing texture and color.


	public void init(Asteroid owner) {
		this.owner = owner;

		transform.parent = owner.transform;					// Set the model's parent to the marble.
		transform.localPosition = new Vector3(0,0,0);		// Center the model on the parent.
		name = "Asteroid Model";									// Name the object.

		Renderer renderer = GetComponent<Renderer> ();
		mat = GetComponent<Renderer>().material;	// Get the material component of this quad object.
		mat.shader = Shader.Find ("Sprites/Default");						// Tell the renderer that our textures have transparency. 

		renderer.sortingOrder = 1;
		mat.mainTexture = Resources.Load<Texture2D>("Textures/asteroid");	// Set the texture.  Must be in Resources folder.
		mat.color = new Color(1,1,1);	
		this.tag = "asteroid";
	}

	void Start () {
		clock = 0f;
	}

	void Update () {
		float x = transform.position.x;
		float y = transform.position.y;
		transform.position = new Vector3 (x, y - Time.deltaTime * 4, 0);
	}

	void OnTriggerEnter2D(Collider2D coll){
		switch (coll.gameObject.tag) {
		case "PlayerLaser":
			coll.gameObject.gameObject.GetComponent<PlayerLaser> ().Hit ();
			Destroy (gameObject);
			break;
		default:
			break;
		}
	}
}





