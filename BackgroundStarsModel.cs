using UnityEngine;
using System.Collections;

public class BackgroundStarsModel : MonoBehaviour {

	private Material mat;
	private BackgroundStars owner;
	private float clock;

	public void init(BackgroundStars owner, float scale) {
		this.owner = owner;
		transform.parent = owner.transform;	
		transform.localPosition = new Vector3(0,0,0);
		name = "Background Star";
		mat = GetComponent<Renderer>().material;	
		mat.shader = Shader.Find ("Sprites/Default");	// Tell the renderer that our textures have transparency. // Get the material component of this quad object.
		mat.mainTexture = Resources.Load<Texture2D>("Textures/box");	// Set the texture.  Must be in Resources folder.
		mat.color = new Color(1,1,1);
		transform.localScale = new Vector3 (scale, scale, scale);
		clock = 0f;
	}

	// Update is called once per frame
	void Update () {
		clock = clock + Time.deltaTime;
		mat.color = new Color (1 + 0.5f * Mathf.Sin (8 * clock), 1 + 0.5f * Mathf.Sin (8 * clock), 1 + 0.5f * Mathf.Sin (8 * clock));
	}
}

