using UnityEngine;
using System.Collections;

public class BeamEnemyModel : MonoBehaviour {

	private Material mat;
	private BeamEnemy owner;

	public void init(BeamEnemy owner) {
		this.owner = owner;
		transform.parent = owner.transform;	
		transform.localPosition = new Vector3(0,0,0);
		name = "Beam Enemy Model";
		mat = GetComponent<Renderer>().material;	
		mat.shader = Shader.Find ("Sprites/Default");	// Tell the renderer that our textures have transparency. // Get the material component of this quad object.
		mat.mainTexture = Resources.Load<Texture2D>("Textures/box");	// Set the texture.  Must be in Resources folder.
		mat.color = new Color(1,1,1);											// Set the color (easy way to tint things).
	}

	// Update is called once per frame
	void Update () {
		if (owner.charging) {
			mat.color = new Color (3, (3 - owner.charge), (3- owner.charge));
			transform.localScale = new Vector3 (1, 1,0);
		} else if (!owner.charging && !owner.entering) {
			mat.color = new Color (1, 0, 0);
			transform.localScale = new Vector3 (1, 20,0);
		}
	}
}